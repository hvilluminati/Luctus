using UnityEngine;

public class EnemyDecorator : MonoBehaviour
{
	public Enemy enemy;
	public SpriteRenderer spriteImage;

	public void Instantiate()
	{
		spriteImage = enemy.spriteImage;
	}


}
