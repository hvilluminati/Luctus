using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace Assets.Scripts
{
	[CreateAssetMenu(fileName = "New Deck", menuName = "Deck")]
	public class Deck : ScriptableObject
	{
		// The public card list that can't be canged from the outside
		public IReadOnlyList<Card> Cards => cards;

		// A deck contains a list of cards
		public List<Card> cards = new List<Card>();

		// Event to tell subscriber that the number in the deck has changed
		public UnityEvent cardsChanged = new UnityEvent();

		private static readonly System.Random rng = new System.Random(); // Singleton

		public void AddCard(Card card)
		{
			cards.Add(card);
			cardsChanged.Invoke();
		}

		public void RemoveCard(Card card)
		{
			cards.Remove(card);
			cardsChanged.Invoke();
		}

		public void ClearDeck()
		{
			cards.Clear();
			cardsChanged.Invoke();
		}

		public void ShuffleDeck()
		{
			int n = cards.Count; // Get the number of cards in the deck

			for (int i = n - 1; i > 0; i--)
			{
				// Pick a random index from 0 to i
				int randomIndex = rng.Next(i + 1);

				// Swap element at i with the element at randomIndex
				Card temp = cards[i];
				cards[i] = cards[randomIndex];
				cards[randomIndex] = temp;
			}
		}
	}
}
