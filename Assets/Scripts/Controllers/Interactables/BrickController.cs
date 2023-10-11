using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BrickController : MonoBehaviour
{
    private Animator brickAnimator;

    protected bool broken;

    private void Start()
    {
        brickAnimator = GetComponent<Animator>();
        broken = false;
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            // If collision from below
            if (other.contacts[0].normal.y > 0)
            {
                brickAnimator.SetTrigger("broken");
                Break();
            }

        }

    }

    public virtual void Break()
    {
        if (!broken) broken = true;
        PlayBrickSound();
    }

    public void PlayBrickSound()
    {
        GetComponent<AudioSource>().Play();
    }

    public virtual void Reset()
    {
        GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Dynamic;
        brickAnimator.SetTrigger("reset");
    }
}
