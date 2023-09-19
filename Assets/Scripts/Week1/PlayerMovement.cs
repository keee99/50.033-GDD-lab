using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{

    // STATE
    private bool onGroundState = true;
    public float speed = 17f;
    public float maxSpeed = 25f;
    public float upSpeed = 20;
    public bool isSpriteFacingRight = true;


    // CACHE
    private Rigidbody2D marioBody;
    private SpriteRenderer marioSprite;

    // CONSTANTS
    private readonly string TAG_GROUND = "Ground";
    private readonly string TAG_ENEMY = "Enemy";

    public Canvas gameCanvas;
    public Canvas gameOverOverlay;

    // Restart Game related
    public TextMeshProUGUI scoreText;

    public GameObject enemies;
    public JumpOverGoomba jumpOverGoomba;


    // Start is called before the first frame update
    void Start()
    {
        // Set to be 30 FPS
        Application.targetFrameRate = 30;
        marioBody = GetComponent<Rigidbody2D>();
        marioSprite = GetComponent<SpriteRenderer>();

        toggleGameOverUI(false);

    }

    // FixedUpdate is called 50 times a second. 
    // Place physics engine stuff here: same frequency as the physics system
    void FixedUpdate()
    {
        MoveHorizontal();
        MoveVertical();
    }

    // !!
    // We do not implement the flipping of Sprite under FixedUpdate 
    // since it has nothing to do with the Physics Engine.
    private void Update()
    {
        // Flip Mario sprite
        if (Input.GetKeyDown("a") && isSpriteFacingRight)
        {
            isSpriteFacingRight = false;
            marioSprite.flipX = true;
        }

        if (Input.GetKeyDown("d") && !isSpriteFacingRight)
        {
            isSpriteFacingRight = true;
            marioSprite.flipX = false;
        }
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag(TAG_GROUND))
        {
            onGroundState = true;
        }
    }


    private void OnCollisionExit2D(Collision2D other)
    {
        if (other.gameObject.CompareTag(TAG_GROUND))
        {
            onGroundState = false;
        }
    }


    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag(TAG_ENEMY))
        {
            Time.timeScale = 0.0f; // Freezes time
            toggleGameOverUI(true);
        }
    }

    private void MoveHorizontal()
    {
        float moveHorizontal = Input.GetAxisRaw("Horizontal");

        // Move Mario if horizontal input keys are pressed
        if (Mathf.Abs(moveHorizontal) > 0)
        {
            Vector2 movement = new Vector2(moveHorizontal, 0);

            // Limit mario to max speed
            // if (marioBody.velocity.magnitude < maxSpeed)
            if (Mathf.Abs(marioBody.velocity.x) < maxSpeed)
            {
                marioBody.AddForce(movement * speed);
            }
        }

        // Stops Mario once the key is lifted
        if (Input.GetKeyUp(KeyCode.A) || Input.GetKeyUp(KeyCode.D))
        {
            // marioBody.velocity = Vector2.zero;
            marioBody.velocity = new Vector2(0, marioBody.velocity.y);

        }

    }

    private void MoveVertical()
    {
        if (Input.GetKeyDown(KeyCode.Space) && onGroundState)
        {
            marioBody.AddForce(Vector2.up * upSpeed, ForceMode2D.Impulse);
            onGroundState = false;
        }
    }

    private void toggleGameOverUI(bool toggle)
    {
        gameOverOverlay.enabled = toggle;
        foreach (TextMeshProUGUI textComponent in gameOverOverlay.GetComponentsInChildren<TextMeshProUGUI>())
        {
            if (textComponent.CompareTag("Score"))
            {
                textComponent.text = scoreText.text;
                break;
            }
        }
    }


    private void ResetGame()
    {
        toggleGameOverUI(false);

        // Reset Position
        marioBody.transform.position = new Vector3(-4.46f, -2.5f, 0.0f);
        marioBody.velocity = Vector3.zero;

        isSpriteFacingRight = true;
        marioSprite.flipX = false;

        ResetScoreText();


        foreach (Transform eachChild in enemies.transform)
        {
            ResetEnemy(eachChild);
        }

        jumpOverGoomba.score = 0;

    }


    private void ResetScoreText()
    {
        scoreText.text = "Score: 0";
    }

    private void ResetEnemy(Transform enemy)
    {
        EnemyMovement childMovement = enemy.GetComponent<EnemyMovement>();
        // enemy.transform.localPosition = enemy.GetComponent<EnemyMovement>().startPosition;
        childMovement.Reset();
    }


    /// Callback method: public void with 1 or less params
    public void RestartButtonCallback(int Input)
    {
        ResetGame();
        Time.timeScale = 1.0f;
    }


}
