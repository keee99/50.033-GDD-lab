using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class EnemyDeath3D : MonoBehaviour
{

    private readonly string TAG_PLAYER = "Player";
    bool alive = true;

    Animator animator;
    Rigidbody rb;
    AudioSource enemyDeathSound;

    public UnityEvent death;


    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody>();
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

    public void Death3D()
    {
        PlayDeathSound();
        gameObject.tag = "Obstacles";
        gameObject.GetComponent<EnemyMovement3D>().Stop();
        rb.velocity = Vector3.zero;
        animator.SetTrigger("death");
        alive = false;
    }

    public void Death()
    {
        death.Invoke();
    }


    public void Reset()
    {
        gameObject.tag = "Enemy";
        gameObject.SetActive(true);
        alive = true;
        animator.Play("GoombaWalk");

    }


}
