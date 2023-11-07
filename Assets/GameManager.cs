using Assets.Scripts;
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

	public GameObject enemies;


	public void Start()
	{
		//turnText.text = "Player's Turn";
		// TODO: Create deck in new game
		if (currentDeck.cards.Count == 0)
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



	public void OnApplicationQuit()
	{
		currentDeck.cards.Clear();
	}
}
