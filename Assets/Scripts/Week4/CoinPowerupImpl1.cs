using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinPowerupImpl : BasePowerup
{
    // setup this object's type
    // instantiate variables

    CoinController controller;

    protected override void Start()
    {
        base.Start(); // call base class Start()
        type = PowerupType.Coin;
        controller = GetComponent<CoinController>();
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Player") && hasSpawned)
        {
            // ApplyPowerUp(other.gameObject.GetComponent<PowerupApplicable>());
            DestroyPowerUp();
        }
    }

    public override void SpawnPowerUp()
    {
        spawned = true;
        DestroyPowerUp();
    }

    public override void ApplyPowerUp(MonoBehaviour target)
    {
        // throw new System.NotImplementedException();
    }
}
