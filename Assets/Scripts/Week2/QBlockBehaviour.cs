using UnityEngine;

public class QBlockBehaviour : MonoBehaviour
{

    private Animator animator;

    public CoinController coin;
    private Rigidbody2D rb;
    private bool isBroken = false;

    private Vector3 originalPosition;

    private bool isCollisionFromBottom = true;



    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        Debug.Log(rb);

        originalPosition = transform.position;

    }

    // Update is called once per frame
    void Update()
    {

        if (rb.velocity.y < 0 && isBroken && transform.position.y < originalPosition.y)
        {
            Debug.Log("cool");
            DisableSpring();
            transform.position = originalPosition;
        }
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Player") && isCollisionFromBottom && !isBroken)
        {
            BreakBlock();
        }
    }

    public void BreakBlock()
    {
        isBroken = true;
        animator.SetTrigger("broken");
        SpawnItem();
    }

    // Brick may not always spawn coin
    // QBlocks always spawn coins (for now)
    public void SpawnItem()
    {
        coin.PlayJump();

    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        DisableSpring();
        isCollisionFromBottom = false;
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (!isBroken)
        {
            EnableSpring();
        }
        isCollisionFromBottom = true;
    }


    // Callback after coin animation
    public void DisableSpring()
    {
        rb.bodyType = RigidbodyType2D.Static;
    }

    public void EnableSpring()
    {
        rb.bodyType = RigidbodyType2D.Dynamic;
    }

    public void Reset()
    {
        isBroken = false;
        coin.Reset();
    }


}
