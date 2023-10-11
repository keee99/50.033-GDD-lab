using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestionBoxPowerupController : Resettable
{
    public Animator powerupAnimator;
    private Animator questionBoxAnimator;
    public BasePowerup powerup;

    private bool broken;

    private void Start()
    {
        questionBoxAnimator = GetComponent<Animator>();
        broken = false;
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {

            // If collision from below
            if (other.contacts[0].normal.y > 0 && !broken)
            {
                broken = true;
                questionBoxAnimator.SetTrigger("broken");
                powerupAnimator.SetTrigger("spawned");
            }

        }

    }

    public void Disable()
    {
        GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Static;
        transform.localPosition = new Vector3(0, 0, 0);
    }

    public void PlaySound()
    {
        GetComponent<AudioSource>().Play();
    }

    public override void Reset()
    {
        GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Kinematic;
        questionBoxAnimator.SetTrigger("reset");
        powerupAnimator.SetTrigger("reset");
        RegeneratePowerUp();

        broken = false;

    }

    public void RegeneratePowerUp()
    {
        powerup.Reset();
    }
}
