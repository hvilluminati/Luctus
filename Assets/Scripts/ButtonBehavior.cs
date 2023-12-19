using UnityEngine;

public class ButtonBehavior : MonoBehaviour
{
	[SerializeField] private AudioSource click;
	[SerializeField] private SceneLoader sceneLoader;
	public int delayTime = 30;
	public int delay = 0;
	private bool isClicked;

	private void FixedUpdate()
	{
		if (isClicked)
		{
			delay += 1;
			if (delay >= delayTime)
			{
				sceneLoader.LoadSelectedScene();
			}

		}
	}

	public void SceneTransition(int sceneNumber)
	{
		sceneLoader.sceneNumber = sceneNumber;
		isClicked = true;
		click.Play();
	}
}
