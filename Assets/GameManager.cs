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

	public GameObject[] enemies;
	public List<EnemyBehavior> enemyBehaviors;



	public void Start()
	{
		turnManager.beginPlayerTurn.AddListener(PlayerTurn);
		// Needs to be refactored
		enemies = GameObject.FindGameObjectsWithTag("Enemy");
		foreach (var enemy in enemies)
		{
			enemyBehaviors.Add(enemy.GetComponent<EnemyBehavior>());
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

	public void OnApplicationQuit()
	{
		currentDeck.ClearDeck();
		discardPile.ClearDeck();
	}
}
