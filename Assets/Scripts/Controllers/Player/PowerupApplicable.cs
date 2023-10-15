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
        Debug.Log("power up effect: " + powerup.powerUpType.ToString());
    }

    public void PlayPowerUpSound()
    {
        if (powerUpSound != null)
        {
            GetComponent<AudioSource>().PlayOneShot(powerUpSound);
        }
    }

    private void Start()
    {

    }

}