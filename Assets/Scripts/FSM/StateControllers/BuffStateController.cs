using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuffStateController : StateController
{

    public BuffType currentBuffType = BuffType.Default;
    public BuffState shouldBeNextState = BuffState.Default;

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
        Debug.Log("STAR STAR STAR");
        // spriteRenderer = GetComponent<SpriteRenderer>();
        // StartCoroutine(BlinkSpriteRenderer());
    }

    public void SetAudioEffect()
    {
        Debug.Log("DING DING DING DIDING DIDING DING DING");
    }
    // private IEnumerator BlinkSpriteRenderer()
    // {
    //     spriteRenderer = GetComponent<SpriteRenderer>();
    //     while (string.Equals(currentState.name, "InvincibleSmallMario", StringComparison.OrdinalIgnoreCase))
    //     {
    //         // Toggle the visibility of the sprite renderer
    //         spriteRenderer.enabled = !spriteRenderer.enabled;

    //         // Wait for the specified blink interval
    //         yield return new WaitForSeconds(gameConstants.flickerInterval);
    //     }

    //     spriteRenderer.enabled = true;
    // }

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