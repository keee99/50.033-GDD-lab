using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class JumpOverGoomba : MonoBehaviour
{

    public Transform enemyLocation;
    public TextMeshProUGUI scoreText;

    private bool onGroundState;

    [System.NonSerialized] public int score = 0; // Dont show in inspector

    private bool countScoreState = false;
    public Vector3 boxSize;
    public float maxDistance;
    public LayerMask layerMask;

    private void FixedUpdate()
    {

        UpdateStates();
        PerformScoring();

    }

    private void UpdateStates()
    {
        // In the moment that mario starts jumping from the Ground.
        // IsOnGround() returns false for Ground-tagged obstacles, true only
        // for Ground layer, thus jumping from obstacles will NOT shift onGroundState to false.
        // This works for now but may be a problem if enemies are on obstacles.
        if (Input.GetKeyDown(KeyCode.Space) && IsOnGround())
        {
            onGroundState = false;
            countScoreState = true;
        }
    }

    private void PerformScoring()
    {
        // When jumping: off the ground and space pressed: 
        // While off the ground, scoring is enabled 
        if (!onGroundState && countScoreState)
        {
            // Goomba is near below Mario and we haven't registered our score, increment score
            if (Mathf.Abs(transform.position.x - enemyLocation.position.x) < 0.5f)
            {
                countScoreState = false;
                score++;
                UpdateScoreText();
            }
        }

    }

    private void UpdateScoreText()
    {
        string scoreString = "Score: " + score.ToString();
        scoreText.text = scoreString;
    }


    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Ground"))
        {
            onGroundState = true;
        }
    }

    /// <summary>
    /// Returns true if a box casted below Mario collides with the Ground Layer
    /// Ground layer does not include the obstacles, thus returns false for collision
    /// with obstacles tagged as Ground.
    /// </summary>
    /// <returns></returns>
    private bool IsOnGround()
    {
        if (Physics2D.BoxCast(
                transform.position, // Origin of the box
                boxSize,  // Box size
                0, // Angle of box in degrees
                -transform.up, // Direction of the box
                maxDistance,  // The maximum distance over which to cast the box.
                layerMask)) // Layer mask filter to detect Colliders only on certain layers.
        {
            return true;
        }

        return false;
    }


    private void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawCube(transform.position - transform.up * maxDistance, boxSize);
    }


}
