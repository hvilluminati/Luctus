using UnityEngine;

public class RandomSoundPlayer : MonoBehaviour
{
	public AudioClip[] soundEffects;
	public float minDelay = 5f;
	public float maxDelay = 15f;

	private float timer;
	private AudioSource audioSource;

	void Start()
	{
		audioSource = gameObject.AddComponent<AudioSource>();
		timer = Random.Range(minDelay, maxDelay);
		audioSource.volume = 0.5f;
	}

	void Update()
	{
		timer -= Time.deltaTime;
		if (timer <= 0f)
		{
			PlayRandomSound();
			timer = Random.Range(minDelay, maxDelay); // Reset the timer
		}
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
}
