using UnityEngine;

public class RandomSoundPlayer : MonoBehaviour
{
	public AudioClip[] soundEffects;
	public float minDelay = 5f;
	public float maxDelay = 15f;

	private float timer;
	private AudioSource audioSource;
	private Transform playerTransform; // Used for sound attenuation
	public float maxHearingDistance = 25.0f; // Distance from where sound can be heard

	void Start()
	{
		audioSource = gameObject.AddComponent<AudioSource>();
		timer = Random.Range(minDelay, maxDelay);
		//audioSource.volume = 0.5f;
		GameObject playerGameObject = GameObject.FindWithTag("Player");
		if (playerGameObject != null)
		{
			playerTransform = playerGameObject.transform;
		}
	}

	void Update()
	{
		float distance = Vector2.Distance(transform.position, playerTransform.position);
		audioSource.volume = CalculateVolumeBasedOnDistance(distance);
		//Debug.Log($"Volumes is: {audioSource.volume}");

		timer -= Time.deltaTime;
		if (timer <= 0f)
		{
			PlayRandomSound();
			timer = Random.Range(minDelay, maxDelay); // Reset the timer
		}
	}

	private float CalculateVolumeBasedOnDistance(float distance)
	{
		return Mathf.Clamp(2 - (distance / maxHearingDistance), 0, 1);
	}

	void PlayRandomSound()
	{
		if (soundEffects.Length > 0)
		{
			int index = Random.Range(0, soundEffects.Length);
			audioSource.clip = soundEffects[index];
			audioSource.Play();
		}
	}

	private void OnDestroy()
	{

	}
}
