using Assets.Scripts;
using DG.Tweening;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class EnemyBehavior : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
	public Enemy enemy;
	public RectTransform healthBar;
	private int currentHealth;
	[SerializeField] private bool isHighlighted = false;
	private RectTransform enemyTransform;
	private Vector3 originalScale;

	public bool pointerIsOn = false;

	public List<CardInteraction> cardsInteractions;


	private void Start()
	{
		enemy = ScriptableObject.CreateInstance<Enemy>();
		enemy.health = 50; // Shall be deleted at some point
		enemyTransform = GetComponent<RectTransform>();

		originalScale = enemyTransform.localScale;

		currentHealth = enemy.health;
	}

	public void OnPointerEnter(PointerEventData eventData)
	{
		HighlightEnemy();
	}

	public void OnPointerExit(PointerEventData eventData)
	{
		UnhighlightEnemy();
	}



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

			GameObject[] cards = GameObject.FindGameObjectsWithTag("Card");
			foreach (var card in cards)
			{
				card.GetComponent<CardInteraction>().selectedEnemy = null;
			}
		}
	}

	public bool turnIsStarted = false;
	public float timer = 0;
	public float delay = 3;
	public bool turnFinished = false;

	void Update()
	{
		if (turnIsStarted)
		{
			timer += Time.deltaTime;
			if (timer >= delay)
			{
				DoDamage();
				turnFinished = true;

				turnIsStarted = false;
				timer = 0;
			}
		}
	}

	public void StartTurn()
	{
		turnIsStarted = true;
		DoDamage();
	}
	public void DoDamage()
	{
		Debug.Log("i have made damage");

	}
}


