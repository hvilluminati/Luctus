using UnityEngine;
using UnityEngine.UI;

public class EnemyDecorator : MonoBehaviour
{
	public Enemy enemy;
	public Sprite spriteImage;
	public Image image;


	private void Start()
	{
		image.sprite = spriteImage;
	}

}
