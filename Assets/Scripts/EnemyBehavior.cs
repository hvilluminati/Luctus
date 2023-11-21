using Assets.Scripts;
using DG.Tweening;
using UnityEngine;
using UnityEngine.EventSystems;

public class EnemyBehavior : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
	public Enemy enemy;
	public RectTransform healthBar;
	public TurnManager turnManager;

	private int currentHealth;
	private bool isHighlighted = false;
	private RectTransform enemyTransform; // Used for animation
	private Vector3 originalScale;





	private void Start()
	{
		enemy = ScriptableObject.CreateInstance<Enemy>();
		enemy.health = 50; // Shall be deleted at some point
		enemyTransform = GetComponent<RectTransform>();

		originalScale = enemyTransform.localScale;

		currentHealth = enemy.health;

		turnManager.beginEnemyTurn.AddListener(beginTurnHandler);
	}

	private void beginTurnHandler()
	{
		DoDamage();
	}



	public void DoDamage()
	{
		Debug.Log("i have made damage");
		turnManager.StartPlayerTurn();
	}

	public void TakeDamage(int damage)
	{
		UnhighlightEnemy();
		Debug.Log("Enemy took damage: " + damage);
		enemyTransform.DOShakePosition(2f, new Vector3(3, 0, 0), 1, 0); // Shake enemy
		currentHealth -= damage; // Loose health
		if (currentHealth <= 0)
		{
			currentHealth = 0;
			// End scene
		}
		float healthBarScale = (float)currentHealth / enemy.health;
		healthBar.DOScaleX(healthBarScale, 0.3f); // Scale healthbar to lost health
	}


	public void HighlightEnemy()
	{

		if (GameObject.FindWithTag("Arrow") != null)
		{
			isHighlighted = true;
			enemyTransform.DOScale(originalScale * 1.1f, 0.3f).SetLoops(-1, LoopType.Yoyo);

			GameObject[] cards = GameObject.FindGameObjectsWithTag("Card");
			foreach (var card in cards)
			{
				card.GetComponent<CardInteraction>().enemieBehaviour = this;
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
				card.GetComponent<CardInteraction>().enemieBehaviour = null;
			}
		}
	}

	public void OnPointerEnter(PointerEventData eventData)
	{
		HighlightEnemy();
	}

	public void OnPointerExit(PointerEventData eventData)
	{
		UnhighlightEnemy();
	}

	public void OnDestroy()
	{
		turnManager.beginEnemyTurn.RemoveListener(beginTurnHandler); // Cleanup
	}
}


