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
		private List<Card> cards = new List<Card>();

		// Event to tell subscriber that the number in the deck has changed
		public UnityEvent cardsChanged = new UnityEvent();

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
	}
}
