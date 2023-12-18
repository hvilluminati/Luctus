using UnityEngine;

public class PlayerMovement : MonoBehaviour
{

	[SerializeField] private Rigidbody2D rb;
	[SerializeField] private Transform player;
	[SerializeField] private Transform groundCheck;
	[SerializeField] private LayerMask groundLayer;

	[SerializeField] private float sideForce;
	[SerializeField] private float upwardForce;
	[SerializeField] private AudioSource walkingSound;
	private PlayerHealth playerHealth;

	private float horizontal;
	private bool isFacingRight = true;
	private bool isFlinching;

	private float x;
	private float y;

	// Start is called before the first frame update
	void Start()
	{
		playerHealth = GetComponent<PlayerHealth>();
		isFlinching = false;
		x = DataManager.instance.x_old;
		y = DataManager.instance.y_old;
		transform.position = new Vector3(x, y, player.position.z);

	}

	private void Update()
	{
		horizontal = Input.GetAxisRaw("Horizontal");

		if (Input.GetButtonDown("Jump") && IsGrounded())
		{
			rb.velocity = new Vector2(rb.velocity.x, upwardForce);
		}

		if (Input.GetButtonUp("Jump") && rb.velocity.y > 0f)
		{
			rb.velocity = new Vector2(rb.velocity.x, rb.velocity.y * 0.5f);
		}
		if (rb.velocity.y != 0) StopSound();
		if (player.position.y < -10)
		{
			this.GetComponent<BoxCollider2D>().enabled = true;
			transform.position = new Vector3(player.position.x - 7, -3, player.position.z);
			if (playerHealth != null)
			{
				playerHealth.TakeDamage(10);
			}
		}

		Flip();
	}

	private void FixedUpdate()
	{
		float horizontalForce = horizontal * sideForce;
		bool isTryingToChangeDirection = (rb.velocity.x > 0f && horizontal < 0f) || (rb.velocity.x < 0f && horizontal > 0f);
		// Apply horizontal movement regardless of whether the player is grounded
		if (IsGrounded())
		{
			rb.velocity = new Vector2(horizontalForce, rb.velocity.y);

			if (Input.GetAxisRaw("Horizontal") != 0 && walkingSound.isPlaying == false)
			{
				PlaySound();
			}
			else if (Input.GetAxisRaw("Horizontal") == 0)
			{
				StopSound();
			}
		}
		else if (!IsGrounded() && isTryingToChangeDirection)
		{
			horizontalForce *= 0.5f;
			rb.velocity = new Vector2(horizontalForce, rb.velocity.y);
		}
		else
		{
			// If in the air and not already moving horizontally, apply a reduced force
			if (Mathf.Abs(rb.velocity.x) < 0.01f) // Adjust this threshold as needed
			{
				rb.velocity = new Vector2(horizontal * (sideForce * 0.5f), rb.velocity.y);
			}
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
		}
	}

	public void Flinch()
	{

		rb.velocity = new Vector2(horizontal * -8, 8);
		isFlinching = false;

	}

	public void setIsFlinching(bool flinchingState)
	{
		isFlinching = flinchingState;
	}

	private void PlaySound()
	{
		walkingSound.Play();
	}

	private void StopSound()
	{
		walkingSound.Stop();
	}

}
