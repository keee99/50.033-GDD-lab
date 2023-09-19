using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{

    private float originalX;
    private float maxOffset = 5f;
    private float enemyPatrolTime = 3.0f;
    public int moveRight = -1;
    private Vector2 velocity;

    private Rigidbody2D enemyBody;

    public Vector3 startPosition = new Vector3(0.0f, 0.0f, 0.0f);


    // Start is called before the first frame update
    void Start()
    {
        enemyBody = GetComponent<Rigidbody2D>();
        originalX = transform.position.x;
        ComputeVelocity();

    }

    public void ComputeVelocity()
    {
        float xSpeed = maxOffset / enemyPatrolTime;
        velocity = new Vector2(moveRight * xSpeed, 0);
    }

    void MoveGoomba()
    {
        Vector2 deltaX = velocity * Time.fixedDeltaTime;
        enemyBody.MovePosition(enemyBody.position + deltaX);
    }

    // Update is called once per frame
    void Update()
    {
        // If not yet in the max offset
        if (Mathf.Abs(enemyBody.position.x - originalX) < maxOffset)
        {
            MoveGoomba();
        }
        else
        {
            moveRight *= -1;
            ComputeVelocity();
            MoveGoomba();
        }
    }
}
