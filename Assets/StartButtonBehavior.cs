using UnityEngine;

public class StartButtonBehavior : MonoBehaviour
{
	[SerializeField] private AudioSource click;
	[SerializeField] private SceneLoader sceneLoader;
	public bool isClicked;
	public int delayTime = 30;
	public int delay = 0;

	private void FixedUpdate()
	{
		if (isClicked)
		{
			delay += 1;
			if (delay >= delayTime)
			{
				sceneLoader.LoadNextScene();
			}

		}
	}

	public void SceneTransition()
	{
		isClicked = true;
		click.Play();
		sceneLoader.LoadSelectedScene();
	}
}
