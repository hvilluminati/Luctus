using Assets.Scripts;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using Random = UnityEngine.Random;

public class GameManager : MonoBehaviour
{
	[SerializeField]
	private int initialDeckAmount = 10;
	[SerializeField]
	private int maxDrawnCards = 5;
	[SerializeField]
	private int drawnCardsSpacing = 160;
	[SerializeField]
	private int drawnCardsYPosition = 300;
	[SerializeField]
	public TMP_Text turnText;

	public RectTransform canvasRectransform;
	public Transform drawnCardsHolder;
	public int availableCardSlots => maxDrawnCards - drawnCardsHolder.childCount;
	public Deck currentDeck;
	public Card[] cardTypes;
	public CardDecorator cardPrefab;

	public TurnManager turnManager;

	public GameObject[] enemies;
	public List<EnemyBehavior> enemyBehaviors;
<<<<<<< HEAD

=======
>>>>>>> parent of 82bb07c (Merge pull request #31 from hvilluminati/feature/damage-animation)


	public void Start()
	{
<<<<<<< HEAD
		turnManager.beginPlayerTurn.AddListener(PlayerTurn);
		// Needs to be refactored
=======
>>>>>>> parent of 82bb07c (Merge pull request #31 from hvilluminati/feature/damage-animation)
		enemies = GameObject.FindGameObjectsWithTag("Enemy");
		foreach (var enemy in enemies)
		{
			enemyBehaviors.Add(enemy.GetComponent<EnemyBehavior>());
		}

<<<<<<< HEAD
		StartTurn();
	}

	private void PlayerTurn()
	{
		DrawCard();
	}

	private void StartTurn() // First turn of the game
	{
		// Draw 5 cards
		for (int i = 0; i < 5; i++)
=======
		turnManager.gm = this;
		//turnText.text = "Player's Turn";
		// TODO: Create deck in new game
		if (currentDeck.cards.Count == 0)
>>>>>>> parent of 82bb07c (Merge pull request #31 from hvilluminati/feature/damage-animation)
		{
			CreateDeck();
		}

		if (turnManager.turn == TurnState.princessTurn)
		{
			DrawCard();
			DrawCard();
			DrawCard();
			DrawCard();
			DrawCard();
		}

		// Princess start turn
	}

	public void CreateDeck()
	{
		for (int i = 0; i < initialDeckAmount; i++)
		{
			Card card = cardTypes[Random.Range(0, cardTypes.Length)];
			currentDeck.cards.Add(card);
		}
	}

	public void DrawCard()
	{
		if (availableCardSlots > 0)
		{
			Card drawnCard = currentDeck.cards[0];
			currentDeck.cards.Remove(drawnCard);
			CardDecorator cardInstance = Instantiate(cardPrefab, drawnCardsHolder);
			cardInstance.card = drawnCard;
			cardInstance.GetComponent<CardInteraction>().canvasRectransform = canvasRectransform;

			cardInstance.Instantiate();

			cardInstance.transform.localScale = Vector3.one * 0.75f;

			// Move all cards into position
			int drawnCardsCount = drawnCardsHolder.childCount;
			int xStartPos = 0 - (drawnCardsCount - 1) * (drawnCardsSpacing / 2);

<<<<<<< HEAD
		// Move all cards into position
		int drawnCardsCount = drawnCardsHolder.childCount;
		int xStartPos = 0 - (drawnCardsCount - 1) * (drawnCardsSpacing / 2);

		for (int i = 0; i < drawnCardsCount; i++)
		{
			RectTransform child = drawnCardsHolder.GetChild(i).GetComponent<RectTransform>();
			int xPos = xStartPos + i * drawnCardsSpacing;

			child.anchoredPosition = new Vector2(xPos, drawnCardsYPosition);

		}
		cardInteraction.cardUsed.AddListener(CardUsedHandler); // Subscribe to know when a card is used

	}


	public void CardUsedHandler(CardInteraction cardInteraction)
	{
		cardInteraction.cardUsed.RemoveListener(CardUsedHandler); // Cleanup
		var usedCard = cardInteraction.GetComponent<CardDecorator>().card; // Get used card
		drawnCardGameObjects.Remove(cardInteraction.gameObject);
		discardPile.AddCard(usedCard); // Add card to discardpile
		cardInteraction.GetComponent<RectTransform>().DOKill();
		DestroyImmediate(cardInteraction.gameObject); // Cleanup

		turnManager.EndPlayerTurn(); // End player turn after card is used
=======
			for (int i = 0; i < drawnCardsCount; i++)
			{
				RectTransform child = drawnCardsHolder.GetChild(i).GetComponent<RectTransform>();
				int xPos = xStartPos + i * drawnCardsSpacing;

				child.anchoredPosition = new Vector2(xPos, drawnCardsYPosition);

			}
		}
		//else
		//turnText.text = "No more card space";
	}

	bool enemyTurnInProgress = false;

	private void Update()
	{
		if (enemyTurnInProgress)
		{
			bool completion = true;
			foreach (var enemy in enemyBehaviors)
			{
				if (!enemy.turnFinished)
				{
					completion = false;
				}
			}
			if (completion)
			{
				Debug.Log("Start player turn");
				turnManager.StartPlayerTurn();
				enemyTurnInProgress = false;
				foreach (var enemy in enemyBehaviors) enemy.turnFinished = false;
			}
		}

	}

	public void EnemyTurn()
	{
		Debug.Log("Enemy Turn");
		foreach (var enemy in enemyBehaviors)
		{
			enemyTurnInProgress = true;
			enemy.StartTurn();
		}


>>>>>>> parent of 82bb07c (Merge pull request #31 from hvilluminati/feature/damage-animation)
	}

	public void OnApplicationQuit()
	{
		currentDeck.cards.Clear();
	}
}
