using UnityEngine;

public class EnemyMovementBird : MonoBehaviour
{
	public float velocity;
	public bool track = false;
	public Transform startingPoint;
	private GameObject player;
	[SerializeField] private AudioSource flapSound;

	void Start()
	{
		player = GameObject.FindGameObjectWithTag("Player");
		flapSound.volume = 0.6f;
	}


    void Update()
    {
        if(track)
        {
            Track();
            Flip();
        }
        else if (transform.position != null)
        {
            Return();
        }
    }

	private void Return()
	{
		transform.position = Vector2.MoveTowards(transform.position, startingPoint.position, velocity * Time.deltaTime);
		flapSound.Stop();
	}
	private void Track()
	{
		transform.position = Vector2.MoveTowards(transform.position, player.transform.position, velocity * Time.deltaTime);
		if (!flapSound.isPlaying)
		{
			flapSound.Play();
		}
	}

	private void Flip()
	{
		if (transform.position.x > player.transform.position.x)
		{
			transform.rotation = Quaternion.Euler(0, 180, 0);
		}
		else
		{
			transform.rotation = Quaternion.Euler(0, 0, 0);
		}
	}

	private void OnBecameVisible()
	{
		enabled = true;
	}

	private void OnBecameInvisible()
	{
		flapSound.Stop();
		enabled = false;
	}

}
