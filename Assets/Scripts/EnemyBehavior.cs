using Assets.Scripts;
using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class EnemyBehavior : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{

	public Enemy enemy;
	public Image healthBar;
	public TMP_Text healthNumber;
	public TurnManager turnManager;
	public CheckCardEffects checkCardEffects;


	private int currentHealth;
	private bool isHighlighted = false;
	private RectTransform enemyTransform; // Used for animation
	private Vector3 originalScale;
	[SerializeField] public EnemyAttack enemyAttack;
	public UnityEvent enemyDead = new UnityEvent();

	private int statusDamage = 0;
	private int bleedDamage;
	private int bleedCounter;
	private int burnDamage;
	private int burnCounter;

	private void Start()
	{
		enemy = ScriptableObject.CreateInstance<Enemy>();
		InitializeEnemyFromLastCollided();
		enemyTransform = GetComponent<RectTransform>();

		originalScale = enemyTransform.localScale;

		currentHealth = enemy.type.GetHealth();
		enemy.charge = 0;

		turnManager.beginEnemyTurn.AddListener(beginTurnHandler);

		healthNumber.text = currentHealth.ToString();
	}

	private void Update()
	{
		healthNumber.text = currentHealth.ToString();
		if (currentHealth <= 0)
		{
			enemyDead.Invoke();
		}
	}

	private void InitializeEnemyFromLastCollided()
	{
		int lastCollidedID = DataManager.instance.lastEnemyCollidedID;
		EnemyType collidedEnemy = GetEnemyTypeFromDataManager(lastCollidedID);

		if (collidedEnemy != null)
		{
			enemy.health = collidedEnemy.GetHealth();
		}
		else
		{
			enemy.health = 20; // Default value if no enemy found
		}
	}


	private EnemyType GetEnemyTypeFromDataManager(int enemyID)
	{
		foreach (var enemyState in DataManager.instance.enemies)
		{
			if (enemyState.enemyType.GetEnemyID() == enemyID)
			{
				return enemyState.enemyType;
			}
		}
		return null;
	}

	private void beginTurnHandler()
	{
		DoDamage();
	}


	public void DoDamage()
	{
		Debug.Log("I have taken damage");

		turnManager.StartPlayerTurn(); // Start player turn
	}

	public void ManageCard(int damage, DamageType damageType, int statusDuration)
	{
		if (damageType == DamageType.Normal)
		{
			TakeDamage(statusDamage + damage);
		}

		else if (damageType == DamageType.Poison)
		{
			Debug.Log("Enemy poisoned.");
			checkCardEffects.isPoisoned = true;
			statusDamage += damage;
			TakeDamage(statusDamage);
		}

		else if (damageType == DamageType.Bleed)
		{
			bleedDamage += damage;
			bleedCounter += statusDuration;
			Debug.Log("Enemy bleeding.");
			checkCardEffects.isBleeding = true;

			statusDamage += damage;
			TakeDamage(statusDamage);
		}

		else if (damageType == DamageType.Fire)
		{
			burnDamage += damage;
			burnCounter += statusDuration;
			Debug.Log("Enemy burned.");
			checkCardEffects.isBurning = true;
			//deal with reduce atk
			statusDamage += damage;
			TakeDamage(statusDamage);
		}

		else if (damageType == DamageType.Frost)
		{
			Debug.Log("Enemy frozen.");
			checkCardEffects.isFreezing = true;
			//deal with freezing
			TakeDamage(statusDamage);
		}

		CheckStatus();
	}

	public void TakeDamage(int damage)
	{
		UnhighlightEnemy();
		Debug.Log("Enemy took damage: " + damage);
		enemyTransform.DOShakePosition(2f, new Vector3(3, 0, 0), 1, 0); // Shake enemy
		currentHealth -= damage; // Loose health
		healthBar.fillAmount = (float)currentHealth / enemy.health;
		if (currentHealth <= 0)
		{
			UnhighlightEnemy();
			Debug.Log("Enemy took damage: " + damage);

			// Shake enemy on damage
			enemyTransform.DOShakePosition(0.5f, new Vector3(20, 0, 0), 5, 45f);

			currentHealth -= damage; // Loose health
			if (currentHealth <= 0)
			{
				currentHealth = 0;
				// End scene
			}
			float healthBarScale = (float)currentHealth / enemy.health;
			//healthBar.DOScaleX(healthBarScale, 0.3f); // Scale healthbar to lost health
		}

		/*
		float healthBarScale = (float)currentHealth / enemy.health;
		healthBar.DOScaleX(healthBarScale, 0.3f); // Scale healthbar to lost health
		*/

	}

	public void CheckStatus()
	{
		//check bleed
		if (bleedCounter > 1)
		{
			bleedCounter--;
		}
		else
		{
			statusDamage -= bleedDamage;
			bleedDamage = 0;
			checkCardEffects.isBleeding = false;
		}

		//check statusDuration
		if (burnCounter > 1)
		{
			burnCounter--;
		}
		else
		{
			statusDamage -= burnDamage;
			checkCardEffects.isBurning = false;
			burnDamage = 0;
		}
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


