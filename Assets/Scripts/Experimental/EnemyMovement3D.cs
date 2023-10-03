using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement3D : MonoBehaviour
{

    private float originalX;
    private float maxOffset = 5f;
    private float enemyPatrolTime = 3.0f;
    public int moveRightInitial = -1;
    public int moveRightState;
    private Vector3 velocity;

    private Rigidbody enemyBody;

    private Vector3 startPosition;

    public bool stopped;


    // Start is called before the first frame update
    void Start()
    {

        startPosition = transform.localPosition;

        enemyBody = GetComponent<Rigidbody>();
        originalX = transform.position.x;
        moveRightState = moveRightInitial;
        ComputeVelocity();

    }

    public void ComputeVelocity()
    {
        float xSpeed = maxOffset / enemyPatrolTime;
        velocity = new Vector2(moveRightState * xSpeed, 0);
    }

    void MoveGoomba()
    {
        Vector3 deltaX = velocity * Time.fixedDeltaTime;
        enemyBody.MovePosition(enemyBody.position + deltaX);
    }

    // Update is called once per frame
    void Update()
    {
        if (stopped)
        {
            return;
        }

        // If not yet in the max offset
        if (Mathf.Abs(enemyBody.position.x - originalX) < maxOffset)
        {
            MoveGoomba();
        }
        else
        {
            moveRightState *= -1;
            ComputeVelocity();
            MoveGoomba();
        }
    }

    public void Reset()
    {
        ResetPosition();
        ResetMovement();
    }

    public void ResetMovement()
    {
        stopped = false;
        moveRightState = moveRightInitial;
        ComputeVelocity();
    }

    private void ResetPosition()
    {
        transform.localPosition = startPosition;
    }

    public void Stop()
    {
        stopped = true;
    }
}
