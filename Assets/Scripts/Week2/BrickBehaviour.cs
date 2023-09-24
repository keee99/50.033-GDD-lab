using UnityEngine;

public class BrickBehaviour : MonoBehaviour
{

    public enum Type
    {
        qBlock,
        brick
    }

    public Type blockType;

    private Animator animator;

    public CoinController coin;
    private Rigidbody2D rb;
    private bool isBroken = false;

    private Vector3 originalPosition;

    private bool isCollisionFromBottom = true;
    private bool hasItem;



    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();

        originalPosition = transform.position;
        InitHasItem();
    }

    private bool InitHasItem()
    {
        if (blockType.Equals(Type.qBlock))
        {
            hasItem = true;
        }
        else
        {
            hasItem = coin != null;
        }
        return hasItem;
    }

    // Update is called once per frame
    void Update()
    {
        if (rb.velocity.y < 0 && transform.position.y < originalPosition.y)
        {
            if (blockType.Equals(Type.qBlock) && isBroken)
            {
                DisableSpring();
            }
            // DisableSpring();
            transform.position = originalPosition;
        }
    }

    private void OnCollisionEnter2D(Collision2D other)
    {

        // Debug.Log(other.GetContact(0).point);
        if (other.gameObject.CompareTag("Player") && isCollisionFromBottom && !isBroken)
        {
            BreakBlock();
        }
    }

    public void BreakBlock()
    {
        if (blockType.Equals(Type.qBlock) && !isBroken)
        {
            isBroken = true;
            animator.SetTrigger("broken");
        }

        SpawnCoin();
    }

    // Brick may not always spawn coin
    // QBlocks always spawn coins (for now)
    public void SpawnCoin()
    {
        if (hasItem)
            coin.PlayJump();

        hasItem = false;
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
        EnableSpring();
        InitHasItem();
        isBroken = false;
        if (coin != null)
            coin.Reset();

        if (blockType.Equals(Type.qBlock))
        {
            animator.SetTrigger("reset");
        }
    }


}
