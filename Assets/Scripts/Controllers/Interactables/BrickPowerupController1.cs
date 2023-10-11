using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BrickPowerupController : BrickController
{
    public Animator powerupAnimator;

    public BasePowerup powerup;

    public override void Break()
    {
        PlayBrickSound();
        if (!broken)
        {
            broken = true;
            powerupAnimator.SetTrigger("spawned");
        }
    }


    public override void Reset()
    {
        base.Reset();
        powerupAnimator.SetTrigger("reset");
        RegeneratePowerUp();
        broken = false;
    }

    public void RegeneratePowerUp()
    {
        powerup.Reset();
    }
}
