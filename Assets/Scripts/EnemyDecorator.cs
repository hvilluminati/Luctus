using UnityEngine;
using UnityEngine.UI;

public class EnemyDecorator : MonoBehaviour
{
	public Enemy enemy;
	public Sprite spriteImage;
	public Image image;

	public void Instantiate()
	{
		//spriteImage = enemy.spriteImage;
	}


	private void Start()
	{
		image.sprite = spriteImage;
	}

}
