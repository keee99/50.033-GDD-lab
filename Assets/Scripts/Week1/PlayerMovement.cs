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


    // Lab 2: Animation
    public Animator marioAnimator;
    public Transform gameCamera;

    // Lab 2: Audio
    public AudioSource marioAudio;
    public AudioClip marioDeath;
    [System.NonSerialized] public bool alive = true;
    public float deathImpulse = 50;


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
        if (alive)
        {
            MoveHorizontal();
            MoveVertical();
        }
    }

    // !!
    // We do not implement the animation related stuff under FixedUpdate 
    // since it has nothing to do with the Physics Engine.
    private void Update()
    {
        // Flip Mario sprite
        if (Input.GetKeyDown("a") && isSpriteFacingRight)
        {
            isSpriteFacingRight = false;
            marioSprite.flipX = true;
            if (marioBody.velocity.x > 0.1f)
            {
                marioAnimator.SetTrigger("onSkid");
            }
        }

        if (Input.GetKeyDown("d") && !isSpriteFacingRight)
        {
            isSpriteFacingRight = true;
            marioSprite.flipX = false;
            if (marioBody.velocity.x < -0.1f)
            {
                marioAnimator.SetTrigger("onSkid");
            }
        }

        marioAnimator.SetFloat("xSpeed", Mathf.Abs(marioBody.velocity.x));
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (
            (
                other.gameObject.CompareTag("Ground")
                || other.gameObject.CompareTag("Enemy")
                || other.gameObject.CompareTag("Obstacles")
            )
            && !onGroundState)
        {
            onGroundState = true;
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

            // We dont put it in onCollisionExit as we don't want jumping when space not pressed
            marioAnimator.SetBool("onGround", onGroundState);
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

        GetComponent<Collider2D>().enabled = true;

        isSpriteFacingRight = true;
        marioSprite.flipX = false;

        ResetScoreText();


        foreach (Transform eachChild in enemies.transform)
        {
            ResetEnemy(eachChild);
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
