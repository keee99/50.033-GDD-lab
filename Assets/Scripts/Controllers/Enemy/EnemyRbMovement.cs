using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyRbMovement : MonoBehaviour
{

    private float originalX;
    public int moveRightInitial = -1;
    private int moveRightState;
    private Vector2 velocity;

    private Rigidbody2D enemyBody;

    public Vector3 startPosition = new Vector3(0.0f, 0.0f, 0.0f);

    int collisionLayerMask = (1 << 7) | (1 << 3) | (1 << 6);

    public float xSpeed = 2.0f;


    // Start is called before the first frame update
    void Start()
    {

        startPosition = transform.localPosition;

        enemyBody = GetComponent<Rigidbody2D>();
        originalX = transform.position.x;
        moveRightState = moveRightInitial;
        ComputeVelocity();

    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (enemyBody == null) return;
        if ((collisionLayerMask & (1 << other.gameObject.layer)) > 0) // pipe layer
        {
            // If colliding from sides
            if (other.contacts[0].normal.x != 0)
            {
                moveRightState = -moveRightState;
                bool goRight = -moveRightState < 0;
                GetComponentInChildren<SpriteRenderer>().flipX = goRight;
                Debug.Log(enemyBody);
                enemyBody.AddForce(Vector2.right * 3 * (goRight ? 1 : -1), ForceMode2D.Impulse);
                ComputeVelocity();
            }

        }
    }

    public void ComputeVelocity()
    {
        velocity = new Vector2(moveRightState * xSpeed, 0);
    }

    void MoveGoomba()
    {
        Vector2 deltaX = velocity * Time.fixedDeltaTime;
        enemyBody.MovePosition(enemyBody.position + deltaX);
    }

    // Update is called once per frame
    void Update()
    {
        ComputeVelocity();
        MoveGoomba();
    }

    public void Reset()
    {
        ResetPosition();
        ResetMovement();
    }

    public void ResetMovement()
    {
        moveRightState = moveRightInitial;
        ComputeVelocity();
    }

    private void ResetPosition()
    {
        transform.localPosition = startPosition;
    }
}
