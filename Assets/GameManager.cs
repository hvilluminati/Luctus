using Assets.Scripts;
using DG.Tweening;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GameManager : MonoBehaviour
{
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
	private List<GameObject> drawnCardGameObjects = new List<GameObject>();
	public int availableCardSlots => maxDrawnCards - drawnCardGameObjects.Count;
	public Deck currentDeck;
	public Deck discardPile;
	public CardDecorator cardPrefab;

	public TurnManager turnManager;
	public EnemyBehavior enemyBehavior;




	public void Start()
	{
		turnManager.beginPlayerTurn.AddListener(PlayerTurn);

		GameObject enemyGameObject = GameObject.FindGameObjectWithTag("Enemy");
		// Get the EnemyBehavior component attached to the enemy GameObject
		if (enemyGameObject != null)
		{
			enemyBehavior = enemyGameObject.GetComponent<EnemyBehavior>();
			if (enemyBehavior != null)
			{
				// Subscribe to the event in the EnemyBehavior class
				enemyBehavior.enemyDead.AddListener(OnEnemyDeadHandler);
			}
			else
			{
				Debug.LogError("EnemyBehavior component not found on the enemy GameObject.");
			}
		}
		else
		{
			Debug.LogError("No GameObject found with the 'Enemy' tag.");
		}

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
		{
			DrawCard();
		}

		// Princess start turn
		Debug.Log("Start player turn");
	}

	public void DrawCard()
	{
		if (availableCardSlots <= 0)
		{
			return;
		}

		Card drawnCard = currentDeck.Cards[0];
		currentDeck.RemoveCard(drawnCard);
		CardDecorator cardInstance = Instantiate(cardPrefab, drawnCardsHolder);
		drawnCardGameObjects.Add(cardInstance.gameObject);
		cardInstance.card = drawnCard;
		var cardInteraction = cardInstance.GetComponent<CardInteraction>();
		cardInteraction.canvasRectransform = canvasRectransform;

		cardInstance.Instantiate();

		cardInstance.transform.localScale = Vector3.one * 0.75f;

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
	}

	public void OnEnemyDeadHandler()
	{
		ReturnDrawnCardsToDeck(); // Return drawn cards to the deck
		Debug.Log("Cards goes back inside deck");
		// Go back to forest scene
	}

	public void ReturnDrawnCardsToDeck()
	{
		foreach (var drawnCardObject in drawnCardGameObjects)
		{
			CardDecorator cardDecorator = drawnCardObject.GetComponent<CardDecorator>();
			if (cardDecorator != null)
			{
				Card drawnCard = cardDecorator.card;
				currentDeck.AddCard(drawnCard); // Adding drawn card back to the deck
				Destroy(drawnCardObject); // Destroy drawn card object from the drawn cards holder
			}
		}
		drawnCardGameObjects.Clear(); // Clear the list of drawn card game objects
	}


	public void OnApplicationQuit()
	{
		currentDeck.ClearDeck();
		discardPile.ClearDeck();
	}
}
