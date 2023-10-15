using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class PowerupApplicable : MonoBehaviour, IPowerupApplicable
{
    public AudioClip powerUpSound;
    public AudioClip powerDownSound;

    public void RequestPowerUpEffect(IPowerup powerup)
    {
        Debug.Log("power up collected: " + powerup.powerUpType.ToString());
        powerup.ApplyPowerUp(this);
    }

    public void PlayPowerUpSound()
    {
        if (powerUpSound != null)
        {
            GetComponent<AudioSource>().PlayOneShot(powerUpSound);
        }
    }

    public void OnPowerupCollected(IPowerup powerup)
    {
        Debug.Log("Play");
        PlayPowerUpSound();
    }

}