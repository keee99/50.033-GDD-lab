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

    public UnityEvent increaseScore;



    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        enemyDeathSound = GetComponent<AudioSource>();

    }


    // private void OnCollisionEnter2D(Collision2D other)
    // {
    //     // If the enemy collides with the player from the top, call the Death() function
    //     if (other.gameObject.CompareTag(TAG_PLAYER) && alive)
    //     {
    //         if (other.contacts[0].normal.y < 0)
    //         {
    //             Death();
    //         }
    //     }
    // }

    public void PlayDeathSound()
    {
        enemyDeathSound.PlayOneShot(enemyDeathSound.clip);
    }

    public void Despawn()
    {
        gameObject.SetActive(false);
    }

    public void Death()
    {
        Death(DeathReaction.Squish);

    }

    public void Death(DeathReaction reaction)
    {
        PlayDeathSound();
        alive = false;
        increaseScore.Invoke();

        if (reaction == DeathReaction.Squish)
        {
            gameObject.tag = "Obstacles";
            rb.bodyType = RigidbodyType2D.Static;
            animator.SetTrigger("death");

        }
        else if (reaction == DeathReaction.Fall)
        {
            rb.bodyType = RigidbodyType2D.Dynamic;
            GetComponent<BoxCollider2D>().enabled = false;
            GetComponent<EdgeCollider2D>().enabled = false;

            gameObject.tag = "Obstacles";
            animator.SetTrigger("death-fall");
            rb.AddForce(Vector2.up * 30, ForceMode2D.Impulse);
        }
    }

    public void Reset()
    {
        rb.bodyType = RigidbodyType2D.Dynamic;
        gameObject.tag = "Enemy";
        gameObject.SetActive(true);
        alive = true;

        GetComponent<BoxCollider2D>().enabled = true;
        GetComponent<EdgeCollider2D>().enabled = true;

        animator.Play("GoombaWalk");

    }

    public enum DeathReaction
    {
        Squish = -1,
        Fall = 0
    }



}
