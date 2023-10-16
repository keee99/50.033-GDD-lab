using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IPowerup
{
    void DestroyPowerUp();
    void SpawnPowerUp();
    void ApplyPowerUp(MonoBehaviour target);

    PowerupType powerUpType { get; }

    bool hasSpawned { get; }




}

public interface IPowerupApplicable
{
    public void RequestPowerUpEffect(IPowerup powerup);
}

public enum PowerupType
{
    Coin = 0,
    SuperMushroom = 1,
    OneUpMushroom = 2,
    Star = 3,
    FireFlower = 4,
    Damage = 99,
    Default = -1
}