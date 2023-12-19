using Assets.Scripts;
using UnityEngine;

public class DeckManager : MonoBehaviour
{
	public static DeckManager instance;
	public Deck currentDeck;
	public Deck collectedDeck;
	public int initialDeckAmount = 10;
	public Card[] starterCards;

	public void CreateNewDeck()
	{
		currentDeck.cards.Clear();
		collectedDeck.cards.Clear();
		for (int i = 0; i < initialDeckAmount; i++)
		{
			Card card = starterCards[i];
			currentDeck.cards.Add(card);
			collectedDeck.cards.Add(card);
		}
	}

	private void Awake()
	{
		if (instance == null)
		{
			instance = this;
			DontDestroyOnLoad(gameObject);
		}
		else
		{
			Destroy(gameObject);
		}
	}

	public void AddCard(Card card)
	{
		currentDeck.cards.Add(card);
		collectedDeck.cards.Add(card);
	}
	public void SetCurrentDeckToCollectedDeck()
	{
		currentDeck.cards.Clear();
		foreach (Card card in collectedDeck.cards)
		{
			currentDeck.cards.Add(card);
		}
	}

	public void ShuffleCurrentDeck()
	{
		currentDeck.ShuffleDeck();
	}

}
