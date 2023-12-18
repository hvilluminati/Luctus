using Assets.Scripts;
using System.Collections.Generic;
using UnityEngine;

public class DisplayCurrentDeck : MonoBehaviour
{
	public GameObject player;
	public GameObject deckButton;
	public GameObject closeButton;

	[SerializeField]
	private int drawnCardsSpacing = 160;
	[SerializeField]
	private int drawnCardsYPosition = 300;

	public Transform drawnCardsHolder;
	private List<GameObject> drawnCardGameObjects = new List<GameObject>();
	public Deck currentDeck;
	public CardDecorator cardPrefab;

	public void Start()
	{
		closeButton.SetActive(false);
	}


	public void DisplayDeck()
	{
		player.GetComponent<PlayerMovement>().enabled = false;

		int deckCardsCount = currentDeck.cards.Count;
		for (int i = 0; i < deckCardsCount; i++)
		{
			Card currentCard = currentDeck.Cards[i];
			CardDecorator cardInstance = Instantiate(cardPrefab, drawnCardsHolder);
			drawnCardGameObjects.Add(cardInstance.gameObject);
			cardInstance.card = currentCard;
			cardInstance.Instantiate();
			cardInstance.transform.localScale = Vector3.one * 0.75f;

			// Move all cards into position
			int drawnCardsCount = drawnCardsHolder.childCount;
			int xStartPos = 0 - (drawnCardsCount - 1) * (drawnCardsSpacing / 2);

			for (int j = 0; j < drawnCardsCount; j++)
			{
				RectTransform child = drawnCardsHolder.GetChild(j).GetComponent<RectTransform>();
				int xPos = xStartPos + j * drawnCardsSpacing;

				child.anchoredPosition = new Vector2(xPos, drawnCardsYPosition);
			}
		}

		deckButton.SetActive(false);
		closeButton.SetActive(true);
	}

	public void CloseDisplay()
	{
		player.GetComponent<PlayerMovement>().enabled = true;
		deckButton.SetActive(true);
		closeButton.SetActive(false);

		foreach (RectTransform child in drawnCardsHolder)
		{
			Destroy(child.gameObject);
		}
	}


}
