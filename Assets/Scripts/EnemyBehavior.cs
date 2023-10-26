using DG.Tweening;
using UnityEngine;

public class EnemyBehavior : MonoBehaviour
{
	public Enemy enemy;
	public Transform healthBar;
	private int currentHealth;

	//public void Start()
	//{
	//	currentHealth = enemy.health;
	//}

	public void TakeDamage(int damage)
	{
		currentHealth -= damage;
		// Shake animation
		if (currentHealth <= 0)
		{
			currentHealth = 0;
			// End scene
		}
		float healthBarScale = (float)currentHealth / enemy.health;
		healthBar.DOScaleX(healthBarScale, 0.3f);
	}




}
