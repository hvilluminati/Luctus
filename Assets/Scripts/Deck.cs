using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace Assets.Scripts
{
	[CreateAssetMenu(fileName = "New Deck", menuName = "Deck")]
	public class Deck : ScriptableObject
	{
		public IReadOnlyList<Card> Cards => cards;

		private List<Card> cards = new List<Card>();
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
