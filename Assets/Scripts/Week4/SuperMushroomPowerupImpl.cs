using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SuperMushroomPowerupImpl : BasePowerup
{

    int collisionLayerMask = (1 << 7) | (1 << 3);

    // setup this object's type
    // instantiate variables
    protected override void Start()
    {
        base.Start(); // call base class Start()
        type = PowerupType.SuperMushroom;
    }



    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Player") && hasSpawned)
        {
            // ApplyPowerUp(other.gameObject.GetComponent<PowerupApplicable>());
            DestroyPowerUp();
        }
        else if ((collisionLayerMask & (1 << other.gameObject.layer)) > 0) // pipe layer
        {

            // If colliding from sides
            if (other.contacts[0].normal.x != 0 && hasSpawned)
            {
                goRight = !goRight;
                GetComponentInChildren<SpriteRenderer>().flipX = !goRight;
                rb.AddForce(Vector2.right * 3 * (goRight ? 1 : -1), ForceMode2D.Impulse);
            }

        }

    }

    public override void SpawnPowerUp()
    {
        spawned = true;
        rb.AddForce(Vector2.right * 3, ForceMode2D.Impulse);
    }

    public override void ApplyPowerUp(MonoBehaviour target)
    {
        // throw new System.NotImplementedException();
    }
}
