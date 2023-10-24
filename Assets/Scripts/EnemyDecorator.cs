using UnityEngine;
using UnityEngine.UI;

public class EnemyDecorator : MonoBehaviour
{
	public Enemy enemy;
	public Image spriteImage;

	public void Instantiate()
	{
		spriteImage.sprite = enemy.spriteImage;
	}


}
