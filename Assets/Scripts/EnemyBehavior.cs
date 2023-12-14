using Assets.Scripts;
using DG.Tweening;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class EnemyBehavior : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
	public Enemy enemy;
	public RectTransform healthBar;
<<<<<<< HEAD
	public TurnManager turnManager;
	public GameObject Exit;

=======
>>>>>>> parent of 82bb07c (Merge pull request #31 from hvilluminati/feature/damage-animation)
	private int currentHealth;
	[SerializeField] private bool isHighlighted = false;
	private RectTransform enemyTransform;
	private Vector3 originalScale;
<<<<<<< HEAD
	private EnemyAttack enemyAttack;

=======

	public bool pointerIsOn = false;

	public List<CardInteraction> cardsInteractions;
>>>>>>> parent of 82bb07c (Merge pull request #31 from hvilluminati/feature/damage-animation)


	private void Start()
	{
		enemy = ScriptableObject.CreateInstance<Enemy>();
<<<<<<< HEAD
		enemy.health = 20; // Shall be deleted at some point
=======
		enemy.health = 50; // Shall be deleted at some point
>>>>>>> parent of 82bb07c (Merge pull request #31 from hvilluminati/feature/damage-animation)
		enemyTransform = GetComponent<RectTransform>();

		originalScale = enemyTransform.localScale;

		currentHealth = enemy.health;
<<<<<<< HEAD
<<<<<<< HEAD
		enemyAttack = new EnemyAttack();
		enemy.charge = 0;
=======

		turnManager.beginEnemyTurn.AddListener(beginTurnHandler);
>>>>>>> e29620c59106179b577dae518312c3d74e01412a
	}

    private void Update()
    {
        if (currentHealth <= 0)
        {
            Exit.GetComponent<SceneLoader>().LoadPrevScene();
        }
    }

    private void beginTurnHandler()
=======
	}

	public void OnPointerEnter(PointerEventData eventData)
	{
		HighlightEnemy();
	}

	public void OnPointerExit(PointerEventData eventData)
>>>>>>> parent of 82bb07c (Merge pull request #31 from hvilluminati/feature/damage-animation)
	{
		UnhighlightEnemy();
	}


<<<<<<< HEAD
	public void DoDamage()
	{
		Debug.Log("i have made damage");

		turnManager.StartPlayerTurn(); // Start player turn
	}
=======
>>>>>>> parent of 82bb07c (Merge pull request #31 from hvilluminati/feature/damage-animation)

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
<<<<<<< HEAD
		healthBar.DOScaleX(healthBarScale, 0.3f); // Scale healthbar to lost health
=======
		healthBar.DOScaleX(healthBarScale, 0.3f);
>>>>>>> parent of 82bb07c (Merge pull request #31 from hvilluminati/feature/damage-animation)
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
<<<<<<< HEAD
<<<<<<< HEAD
=======
>>>>>>> parent of 82bb07c (Merge pull request #31 from hvilluminati/feature/damage-animation)
		if (turnIsStarted)
		{
			timer += Time.deltaTime;
			if (timer >= delay)
			{
<<<<<<< HEAD
				StartTurn();
=======
				DoDamage();
>>>>>>> parent of 82bb07c (Merge pull request #31 from hvilluminati/feature/damage-animation)
				turnFinished = true;

				turnIsStarted = false;
				timer = 0;
			}
		}
<<<<<<< HEAD
=======
		HighlightEnemy();
>>>>>>> e29620c59106179b577dae518312c3d74e01412a
=======
>>>>>>> parent of 82bb07c (Merge pull request #31 from hvilluminati/feature/damage-animation)
	}

	public void StartTurn()
	{
<<<<<<< HEAD
<<<<<<< HEAD
		turnIsStarted = true;
		enemyAttack.AttackHandler(enemy);
	}


=======
		UnhighlightEnemy();
	}

	public void OnDestroy()
=======
		turnIsStarted = true;
		DoDamage();
	}
	public void DoDamage()
>>>>>>> parent of 82bb07c (Merge pull request #31 from hvilluminati/feature/damage-animation)
	{
		Debug.Log("i have made damage");

	}
>>>>>>> e29620c59106179b577dae518312c3d74e01412a
}


