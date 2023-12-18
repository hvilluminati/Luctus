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
	private EnemyType enemyType;

	public GameObject Exit;
	public GameObject choosePanel;
	public Card Card1;
	public Card Card2;
	public GameObject actualCard1;
	public GameObject actualCard2;


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


		//set reward cards
		enemyType = enemyGameObject.GetComponent<EnemyDecorator>().collidedEnemy;
		Card1 = enemyType.GetCard1();
		Card2 = enemyType.GetCard2();
		actualCard1.GetComponent<CardDecoratorSimple>().Initiate(Card1);
		actualCard2.GetComponent<CardDecoratorSimple>().Initiate(Card2);

		StartTurn();
	}

	private void PlayerTurn()
	{
		turnManager.beginPlayerTurn.AddListener(PlayerTurn);
		DrawCard();
		
	}

	private void StartTurn() // First turn of the game
	{
		// Draw 5 cards
		for (int i = 0; i < 5; i++)
		{
			DrawCard();
		}
	}

	public void DrawCard()
	{
		if (availableCardSlots <= 0)
		{
			return;
			// What should happen when you run out of cards?
		}

		Card drawnCard = currentDeck.Cards[0];
		currentDeck.RemoveCard(drawnCard);

		// Deck sprite position
		float deckXPosition = 1100f;
		float deckYPosition = -20f;

		CardDecorator cardInstance = Instantiate(cardPrefab, new Vector3(deckXPosition, deckYPosition, 0f), Quaternion.identity, drawnCardsHolder);
		drawnCardGameObjects.Add(cardInstance.gameObject);
		cardInstance.card = drawnCard;
		var cardInteraction = cardInstance.GetComponent<CardInteraction>();
		cardInteraction.canvasRectransform = canvasRectransform;

		cardInstance.Instantiate();
		cardInstance.transform.localScale = Vector3.one * 0.75f;

		// Move all cards into position
		int drawnCardsCount = drawnCardsHolder.childCount;
		int xStartPos = 0 - (drawnCardsCount - 1) * (drawnCardsSpacing / 2);

		Sequence cardAnimationSequence = DOTween.Sequence();

		for (int i = 0; i < drawnCardsCount; i++)
		{
			RectTransform child = drawnCardsHolder.GetChild(i).GetComponent<RectTransform>();
			int xPos = xStartPos + i * drawnCardsSpacing;

			cardAnimationSequence.Join(child.DOAnchorPos(new Vector2(xPos, drawnCardsYPosition), 1f)
				.SetEase(Ease.InOutSine));

		}

		cardAnimationSequence.OnComplete(() =>
		{
			cardInteraction.cardUsed.AddListener(CardUsedHandler); // Subscribe to know when a card is used
		});

		Debug.Log("hey???");
		turnManager.EndPlayerTurn();

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

		//ChooseCards
		choosePanel.SetActive(true);
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

	public void ChooseCard1()
	{
		DeckManager.instance.AddCard(Card1);
		// Go back to platformer
		Exit.GetComponent<SceneLoader>().LoadPrevScene();
	}

	public void ChooseCard2()
	{
		DeckManager.instance.AddCard(Card2);
		// Go back to platformer
		Exit.GetComponent<SceneLoader>().LoadPrevScene();
	}

	public void OnApplicationQuit()
	{
		currentDeck.ClearDeck();
		discardPile.ClearDeck();
	}
}
