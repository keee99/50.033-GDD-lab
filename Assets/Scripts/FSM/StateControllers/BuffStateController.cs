using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuffStateController : StateController
{

    public BuffType currentBuffType = BuffType.Default;
    public BuffState shouldBeNextState = BuffState.Default;

    public GameConstants gameConstants;

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


    public void SetRendererEffect()
    {
        StartCoroutine(BlinkSpriteRenderer());
    }

    public void SetAudioEffect()
    {
        Debug.Log("DING DING DING DIDING DIDING DING DING");
        // Change the mixer snapshot

    }

    private IEnumerator BlinkSpriteRenderer()
    {
        SpriteRenderer spriteRenderer = GetComponent<SpriteRenderer>();
        Color initialColor = spriteRenderer.color;

        while (string.Equals(currentState.name, "Invincible", StringComparison.OrdinalIgnoreCase))
        {
            // Toggle the sprite renderer to a random color
            spriteRenderer.color = new Color(GetRandomFloat(), GetRandomFloat(), GetRandomFloat(), 1f);

            // Wait for the specified blink interval
            yield return new WaitForSeconds(gameConstants.flickerInterval);
        }
        spriteRenderer.color = initialColor;
        spriteRenderer.enabled = true;
    }

    private float GetRandomFloat()
    {
        return UnityEngine.Random.value;
    }

    public void SetInvincibility()
    {
        Debug.Log("I AM INVINCIBLE");
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