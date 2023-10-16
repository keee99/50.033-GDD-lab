using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class FireFlowerPowerupImpl : BasePowerup
{

    int collisionLayerMask = (1 << 7) | (1 << 3);

    public UnityEvent<IPowerup> OnPowerupAffectsPlayer;
    public UnityEvent<IPowerup> OnPowerupCollected;

    // setup this object's type
    // instantiate variables
    protected override void Start()
    {
        base.Start(); // call base class Start()
        spawned = false;
        type = PowerupType.FireFlower;
        rb.bodyType = RigidbodyType2D.Static;
        GetComponent<BoxCollider2D>().enabled = false;
    }


    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Player") && hasSpawned)
        {
            OnPowerupAffectsPlayer.Invoke(this);
            OnPowerupCollected.Invoke(this);
            DestroyPowerUp();
        }
    }

    public override void SpawnPowerUp()
    {
        spawned = true;
        GetComponent<BoxCollider2D>().enabled = true;

    }


    public override void Reset()
    {
        base.Reset();
        rb.bodyType = RigidbodyType2D.Static;
        GetComponent<BoxCollider2D>().enabled = false;
        GetComponentInChildren<Animator>().SetTrigger("reset");
        transform.localPosition = new Vector3(0, 1, 0);

    }

    public override void ApplyPowerUp(MonoBehaviour i)
    {
        // base.ApplyPowerUp(i);
        MarioStateController mario;
        bool result = i.TryGetComponent(out mario);
        if (result)
        {
            mario.SetPowerup(type);
        }
    }
}
