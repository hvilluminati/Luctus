using Assets.Scripts;
using DG.Tweening;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBehavior : MonoBehaviour//, IPointerUpHandler, IPointerEnterHandler, IPointerExitHandler
{
	public Enemy enemy;
	public RectTransform healthBar;
	private int currentHealth = 1000;
	[SerializeField] private bool isHighlighted = false;
	private RectTransform enemyTransform;
	private Vector3 originalScale;

	public bool pointerIsOn = false;

	public List<CardInteraction> cardsInteractions;


	private void Start()
	{
		enemy = new Enemy();
		enemy.health = 1000;
		enemyTransform = GetComponent<RectTransform>();

		originalScale = enemyTransform.localScale;

		currentHealth = enemy.health;
	}

	//public void OnPointerEnter(PointerEventData eventData)
	//{
	//	Debug.Log("Inside enemy onPointerEnter");
	//	HighlightEnemy();
	//}

	//public void OnPointerExit(PointerEventData eventData)
	//{
	//	Debug.Log("Inside enemy onPointerExit");
	//	UnhighlightEnemy();
	//}



	public void TakeDamage(int damage)
	{
		UnhighlightEnemy();
		Debug.Log("Enemy took damage: " + damage);
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


	public void HighlightEnemy()
	{
		if (!isHighlighted && pointerIsOn)
		{
			isHighlighted = true;
			enemyTransform.DOScale(originalScale * 1.1f, 0.3f).SetLoops(-1, LoopType.Yoyo);

			GameObject[] cards = GameObject.FindGameObjectsWithTag("Card");
			foreach (var card in cards)
			{
				card.GetComponent<CardInteraction>().selectedEnemy = this;
			}
		}
	}

	public void UnhighlightEnemy()
	{
		if (isHighlighted)
		{
			isHighlighted = false;
			enemyTransform.DOKill();
			enemyTransform.localScale = originalScale;
		}
	}

	public void PointerUp()
	{
		Debug.Log("Pointer Up");
		UnhighlightEnemy();
		if (!pointerIsOn)
		{
			TakeDamage(100);
		}
	}
}
