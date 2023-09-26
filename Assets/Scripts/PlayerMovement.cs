using UnityEngine;

public class PlayerMovement : MonoBehaviour
{

    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private Transform groundCheck;
    [SerializeField] private LayerMask groundLayer;

    [SerializeField] private float sideForce;
    [SerializeField] private float upwardForce;
    private float horizontal;
    private bool isFacingRight = true;

    /*public SpriteRenderer spriteRenderer;
    public Sprite[] Lucia;

    public float frameTime;

    private float moveCounter = 0;
    private int i = 0;*/

    private void Update()
    {
        horizontal = Input.GetAxisRaw("Horizontal");

        if (Input.GetButtonDown("Jump") && IsGrounded())
        {
            Debug.Log("Pressed button down? up?");
            rb.velocity = new Vector2(rb.velocity.x, upwardForce);
        }

        if (Input.GetButtonUp("Jump") && rb.velocity.y > 0f)
        {
            Debug.Log("Pressed button up? down?");
            rb.velocity = new Vector2(rb.velocity.x, rb.velocity.y * 0.5f);
        }

        Flip();
        //moveCounter += Time.deltaTime;
    }

    private void FixedUpdate()
    {
        if (IsGrounded())
        {
            rb.velocity = new Vector2(horizontal * sideForce, rb.velocity.y);

          /*  if (moveCounter > frameTime)
            {
                i = (i == 1) ? 0 : 1;
                spriteRenderer.sprite = Lucia[i];
                moveCounter = 0;
            }*/
        }
    }

    private bool IsGrounded()
    {
        return Physics2D.OverlapCircle(groundCheck.position, 0.2f, groundLayer);
    }

    private void Flip()
    {
        // face right and wants to move left OR face left and wants to move right
        if (isFacingRight && horizontal < 0f || !isFacingRight && horizontal > 0f)
        {
            isFacingRight = !isFacingRight;
            Vector3 localScale = transform.localScale;
            localScale.x *= -1f;
            transform.localScale = localScale; //make transform negative
            Debug.Log("Flipped");
        }
    }
}
