using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BasePowerup : MonoBehaviour, IPowerup
{
    protected PowerupType type;
    public bool spawned = false;
    protected bool consumed = false;
    protected bool goRight = true;
    protected Rigidbody2D rb;

    protected virtual void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    public PowerupType powerUpType
    {
        get => type;
    }

    public bool hasSpawned
    {
        get => spawned;
    }

    public abstract void ApplyPowerUp(MonoBehaviour target);
    public abstract void SpawnPowerUp();

    public void DestroyPowerUp()
    {
        // Destroy(gameObject);
        gameObject.SetActive(false);
    }

    public virtual void Reset()
    {
        gameObject.SetActive(true);
        spawned = false;
        consumed = false;
        goRight = true;
        transform.localPosition = new Vector3(0, 1, 0);
    }


}
