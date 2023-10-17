using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class BuffStateController : StateController
{

    public BuffType currentBuffType = BuffType.Default;
    public BuffState shouldBeNextState = BuffState.Default;

    public GameConstants gameConstants;

    public AudioMixer mixer;

    public AudioClip starAudioClip;

    public void SetBuffType(BuffType type)
    {
        currentBuffType = type;
    }

    public override void Start()
    {
        base.Start();
        GameRestart(); // clear powerup in the beginning, go to start state
    }

    // this should be added to the GameRestart EventListener as callback
    public void GameRestart()
    {
        // set the start state
        TransitionToState(startState);
    }


    public void SetEffects()
    {
        StartCoroutine(BlinkSpriteRenderer());
    }

    private IEnumerator BlinkSpriteRenderer()
    {
        SpriteRenderer spriteRenderer = GetComponent<SpriteRenderer>();
        Color initialColor = spriteRenderer.color;

        AudioSource envAudioSource = GameObject.Find("Environment").GetComponent<AudioSource>();
        AudioClip bgmClip = envAudioSource.clip;
        float time = envAudioSource.time;
        envAudioSource.clip = starAudioClip;
        envAudioSource.loop = true;
        envAudioSource.Play();

        while (string.Equals(currentState.name, "Invincible", StringComparison.OrdinalIgnoreCase))
        {
            // Toggle the sprite renderer to a random color
            spriteRenderer.color = new Color(GetRandomFloat(), GetRandomFloat(), GetRandomFloat(), 1f);

            // Wait for the specified blink interval
            yield return new WaitForSeconds(gameConstants.flickerInterval);
        }

        envAudioSource.clip = bgmClip;
        envAudioSource.time = time;
        envAudioSource.Play();

        spriteRenderer.color = initialColor;
        spriteRenderer.enabled = true;
    }

    private float GetRandomFloat()
    {
        return UnityEngine.Random.value;
    }

}


public enum BuffState
{
    Default = -1,
    Invincible = 0,

}

public enum BuffType
{
    Default = -1,
    Star = 0,
}