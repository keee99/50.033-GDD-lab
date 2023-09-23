using UnityEngine;

public class QBlockBehaviour : MonoBehaviour
{

    private Animator animator;

    public CoinController coin;
    private Rigidbody2D rb;
    private bool isBroken;



    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        isBroken = false;
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Player") && !isBroken)
        {
            animator.SetTrigger("broken");
            isBroken = true;

            SpawnItem();

        }
    }

    // Brick may not always spawn coin
    // QBlocks always spawn coins (for now)
    public void SpawnItem()
    {
        coin.PlayJump();

        // Play item animation
        // At the end of animation:
        //      DisableSpring()

    }


    // Callback after coin animation
    public void DisableSpring()
    {
        rb.bodyType = RigidbodyType2D.Static;
    }

    public void PlayCoinSound()
    {
        // Unimplemented
    }

    public void Reset()
    {
        isBroken = false;
        coin.Reset();
    }


}
