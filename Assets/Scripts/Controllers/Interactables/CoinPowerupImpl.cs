using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinPowerupImpl : BasePowerup
{

    protected override void Start()
    {
        base.Start(); // call base class Start()
        rb.bodyType = RigidbodyType2D.Static;
        type = PowerupType.Coin;
    }

    public override void SpawnPowerUp()
    {
        spawned = true;
        ApplyPowerUp(this);
    }

    public override void Reset()
    {
        base.Reset();
        rb.bodyType = RigidbodyType2D.Static;
    }

    public override void ApplyPowerUp(MonoBehaviour target)
    {
        GameManager.Instance.IncreaseScore(1);
    }
}