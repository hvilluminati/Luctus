using DG.Tweening;
using UnityEngine;
using UnityEngine.EventSystems;

public class EnemyBehavior : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
	public Enemy enemy;
	public RectTransform healthBar;
	private int currentHealth;
	private bool isHighlighted = false;
	private RectTransform enemyTransform;
	private Vector3 originalScale;


	private void Start()
	{
		enemyTransform = transform.Find("EnemySprite").GetComponent<RectTransform>();
		Debug.Log(enemyTransform);
		originalScale = enemyTransform.localScale;
	}

	public void OnPointerEnter(PointerEventData eventData)
	{
		Debug.Log("Inside enemy onPointerEnter");
		HighlightEnemy();
	}

	public void OnPointerExit(PointerEventData eventData)
	{
		Debug.Log("Inside enemy onPointerExit");
		UnhighlightEnemy();
	}

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


	private void HighlightEnemy()
	{
		if (!isHighlighted)
		{
			isHighlighted = true;
			enemyTransform.DOScale(originalScale * 1.1f, 0.3f).SetLoops(-1, LoopType.Yoyo);
		}
	}

	private void UnhighlightEnemy()
	{
		if (isHighlighted)
		{
			isHighlighted = false;
			enemyTransform.DOKill();
			enemyTransform.localScale = originalScale;
		}
	}

}
