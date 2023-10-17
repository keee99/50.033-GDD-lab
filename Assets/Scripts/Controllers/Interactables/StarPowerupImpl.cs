using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class StarPowerupImpl : BasePowerup
{

    int collisionLayerMask = (1 << 7) | (1 << 3);
    private bool gogogo = false;

    private int impulse = 4;

    public UnityEvent<IPowerup> OnPowerupAffectsPlayer;
    public UnityEvent<IPowerup> OnPowerupCollected;

    // setup this object's type
    // instantiate variables
    protected override void Start()
    {
        base.Start(); // call base class Start()
        spawned = false;
        type = PowerupType.Star;
        rb.bodyType = RigidbodyType2D.Static;
        GetComponent<BoxCollider2D>().enabled = false;
    }

    private void Update()
    {
        if (gogogo && hasSpawned)
        {
            ApplyImpulse();
            gogogo = false;
        }
    }



    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Player") && hasSpawned)
        {
            OnPowerupAffectsPlayer.Invoke(this);
            OnPowerupCollected.Invoke(this);
            DestroyPowerUp();
        }
        else if ((collisionLayerMask & (1 << other.gameObject.layer)) > 0) // pipe layer
        {
            // Ignore own parent
            if (other.transform.name == GetComponentInParent<Transform>().name)
            {
                return;
            }

            if (other.contacts[0].normal.y != 0 && hasSpawned)
            {
                rb.AddForce(Vector2.up * 10, ForceMode2D.Impulse);
            }
            // If colliding from sides
            if (other.contacts[0].normal.x != 0 && hasSpawned)
            {
                goRight = !goRight;
                GetComponentInChildren<SpriteRenderer>().flipX = !goRight;
                rb.AddForce(Vector2.right * impulse * (goRight ? 1 : -1), ForceMode2D.Impulse);
            }


        }

    }

    public override void SpawnPowerUp()
    {
        StartCoroutine(EnablePhysics());
        spawned = true;

    }

    private void ApplyImpulse()
    {
        rb.AddForce(Vector2.right * impulse, ForceMode2D.Impulse);
    }

    IEnumerator EnablePhysics()
    {
        rb.bodyType = RigidbodyType2D.Dynamic;
        GetComponent<BoxCollider2D>().enabled = true;
        yield return new WaitUntil(() => hasSpawned);
        gogogo = true;
    }


    public override void Reset()
    {
        base.Reset();
        gogogo = false;
        rb.bodyType = RigidbodyType2D.Static;
        GetComponent<BoxCollider2D>().enabled = false;
        GetComponentInChildren<Animator>().SetTrigger("reset");
        transform.localPosition = new Vector3(0, 1, 0);

    }

    public override void ApplyPowerUp(MonoBehaviour i)
    {
        // base.ApplyPowerUp(i);
        BuffStateController mario;
        bool result = i.TryGetComponent(out mario);
        if (result)
        {
            mario.SetBuffType(BuffType.Star);
        }
    }
}
