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
    public GameObject blocks;
    public JumpOverGoomba jumpOverGoomba;


    // Lab 2: Animation
    public Animator marioAnimator;
    public Transform gameCamera;

    // Lab 2: Audio
    public AudioSource marioAudio;
    public AudioClip marioDeath;
    [System.NonSerialized] public bool alive = true;
    public float deathImpulse = 50;

    int collisionLayerMask = (1 << 3) | (1 << 6) | (1 << 7);


    // Lab 3
    private bool moving = false;
    private bool jumpedState = false;


    // Start is called before the first frame update
    void Start()
    {
        // Set to be 30 FPS
        Application.targetFrameRate = 30;
        marioBody = GetComponent<Rigidbody2D>();
        marioSprite = GetComponent<SpriteRenderer>();
        marioAnimator = GetComponent<Animator>();

        toggleGameOverUI(false);

        marioAnimator.SetBool("onGround", onGroundState);

    }

    // FixedUpdate is called 50 times a second. 
    // Place physics engine stuff here: same frequency as the physics system
    void FixedUpdate()
    {
        if (alive && moving)
        {
            Move(isSpriteFacingRight == true ? 1 : -1);
        }
    }

    // !!
    // We do not implement the animation related stuff under FixedUpdate 
    // since it has nothing to do with the Physics Engine.
    private void Update()
    {
        // Flip Mario sprite moved

        marioAnimator.SetFloat("xSpeed", Mathf.Abs(marioBody.velocity.x));
    }

    void FlipMarioSprite(int value)
    {
        if (value == -1 && isSpriteFacingRight)
        {
            isSpriteFacingRight = false;
            marioSprite.flipX = true;
            if (marioBody.velocity.x > 0.1f)
            {
                marioAnimator.SetTrigger("onSkid");
            }
        }
        else if (value == 1 && !isSpriteFacingRight)
        {
            isSpriteFacingRight = true;
            marioSprite.flipX = false;
            if (marioBody.velocity.x < -0.1f)
            {
                marioAnimator.SetTrigger("onSkid");
            }
        }
    }


    private void OnCollisionEnter2D(Collision2D other)
    {
        if (((collisionLayerMask & (1 << other.transform.gameObject.layer)) > 0) & !onGroundState)
        {
            onGroundState = true;
            // update animator state
            marioAnimator.SetBool("onGround", onGroundState);
        }
    }


    private void OnCollisionExit2D(Collision2D other)
    {
        if (other.gameObject.CompareTag(TAG_GROUND))
        {
            onGroundState = false;
        }
    }

    void GameOverScene()
    {
        Time.timeScale = 0.0f;
        toggleGameOverUI(true);
    }


    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag(TAG_ENEMY) && alive)
        {
            marioBody.bodyType = RigidbodyType2D.Static;
            marioAnimator.Play("mario-die");
            marioAudio.PlayOneShot(marioDeath);
            alive = false;
        }
    }

    void Move(int value)
    {
        Vector2 movement = new Vector2(value, 0);

        // Limit mario to max speed
        // if (marioBody.velocity.magnitude < maxSpeed)
        if (Mathf.Abs(marioBody.velocity.x) < maxSpeed)
        {
            marioBody.AddForce(movement * speed);
        }
    }

    public void Jump()
    {
        if (alive && onGroundState)
        {
            marioBody.AddForce(Vector2.up * upSpeed, ForceMode2D.Impulse);
            onGroundState = false;
            jumpedState = true;

            // We dont put it in onCollisionExit as we don't want jumping when space not pressed
            marioAnimator.SetBool("onGround", onGroundState);
        }
    }

    public void JumpHold()
    {
        if (alive && onGroundState)
        {
            marioBody.AddForce(Vector2.up * upSpeed * 30, ForceMode2D.Force);
            jumpedState = false;
        }
    }

    /// <summary>
    /// MoveCheck used as CALLBACK to UnityEvent for horizontal movement from input
    /// </summary>
    /// <param name="value">Int value from input system</param>
    public void MoveCheck(int value)
    {
        if (value == 0)
        {
            moving = false;
        }
        else
        {
            FlipMarioSprite(value);
            moving = true;
            Move(value);
        }
    }

    private void MoveHorizontal()
    {
        // float moveHorizontal = Input.GetAxisRaw("Horizontal");

        // // Move Mario if horizontal input keys are pressed
        // if (Mathf.Abs(moveHorizontal) > 0)
        // {
        //     Vector2 movement = new Vector2(moveHorizontal, 0);

        //     // Limit mario to max speed
        //     // if (marioBody.velocity.magnitude < maxSpeed)
        //     if (Mathf.Abs(marioBody.velocity.x) < maxSpeed)
        //     {
        //         marioBody.AddForce(movement * speed);
        //     }
        // }

        // // Stops Mario once the key is lifted
        // if (Input.GetKeyUp(KeyCode.A) || Input.GetKeyUp(KeyCode.D))
        // {
        //     // marioBody.velocity = Vector2.zero;
        //     marioBody.velocity = new Vector2(0, marioBody.velocity.y);

        // }

    }

    // private void MoveVertical()
    // {
    //     if (Input.GetKeyDown(KeyCode.Space) && onGroundState)
    //     {
    //         marioBody.AddForce(Vector2.up * upSpeed, ForceMode2D.Impulse);
    //         onGroundState = false;

    //         // We dont put it in onCollisionExit as we don't want jumping when space not pressed
    //         marioAnimator.SetBool("onGround", onGroundState);
    //     }
    // }

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

        GetComponent<Collider2D>().enabled = true;

        isSpriteFacingRight = true;
        marioSprite.flipX = false;

        ResetScoreText();
        ResetCamera();


        foreach (Transform eachChild in enemies.transform)
        {
            ResetEnemy(eachChild);
        }

        foreach (Transform eachChild in blocks.transform)
        {
            ResetBlock(eachChild);
        }

        jumpOverGoomba.score = 0;

        marioAnimator.SetTrigger("gameRestart");
        alive = true;

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

    private void ResetBlock(Transform block)
    {
        BlockBehaviour blockBehaviour = block.GetComponentInChildren<BlockBehaviour>();
        blockBehaviour.Reset();
    }

    private void ResetCamera()
    {
        gameCamera.position = new Vector3(0.34f, 2f, -10f);
    }


    /// Callback method: public void with 1 or less params
    public void RestartButtonCallback(int Input)
    {
        ResetGame();
        Time.timeScale = 1.0f;
    }



    // LAB 2: Audio callback
    void PlayJumpSound()
    {
        marioAudio.PlayOneShot(marioAudio.clip);
    }

    void PlayDeathImpulse()
    {
        marioBody.bodyType = RigidbodyType2D.Dynamic;
        GetComponent<Collider2D>().enabled = false;
        marioBody.AddForce(Vector2.up * deathImpulse, ForceMode2D.Impulse);
    }




}
