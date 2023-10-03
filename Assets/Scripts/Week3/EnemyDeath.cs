using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class EnemyDeath : MonoBehaviour
{

    private readonly string TAG_PLAYER = "Player";
    bool alive = true;

    Animator animator;
    Rigidbody2D rb;
    AudioSource enemyDeathSound;

    public UnityEvent death;



    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        enemyDeathSound = GetComponent<AudioSource>();

    }


    private void OnCollisionEnter2D(Collision2D other)
    {
        // If the enemy collides with the player from the top, call the Death() function
        if (other.gameObject.CompareTag(TAG_PLAYER) && alive)
        {
            if (other.contacts[0].normal.y < 0)
            {
                Death();
            }
        }
    }

    public void PlayDeathSound()
    {
        enemyDeathSound.PlayOneShot(enemyDeathSound.clip);
    }

    public void Despawn()
    {
        gameObject.SetActive(false);
    }

    public void Death2D()
    {
        PlayDeathSound();
        gameObject.tag = "Obstacles";
        rb.bodyType = RigidbodyType2D.Static;
        animator.SetTrigger("death");
        alive = false;

    }

    public void Death()
    {
        death.Invoke();
    }

    public void Reset()
    {
        gameObject.SetActive(true);
        gameObject.tag = "Enemy";
        rb.bodyType = RigidbodyType2D.Kinematic;
        alive = true;
        animator.Play("GoombaWalk");

    }


}
