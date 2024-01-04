using UnityEngine;

public class ExitButtonBehavior : MonoBehaviour
{
	[SerializeField] private AudioSource click;
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
                    Application.Quit();
			}

		}
	}
}
