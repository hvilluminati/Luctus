using TMPro;
using UnityEngine;

namespace Assets.Scripts
{
	public class DeckDisplay : MonoBehaviour
	{
		public TMP_Text cardCountField;

		public Deck deck;

		public void CardChangedHandler()
		{
			cardCountField.text = deck.Cards.Count.ToString();
		}

		public void Start()
		{
			deck.cardsChanged.AddListener(CardChangedHandler);
		}

		public void OnDestroy()
		{
			deck.cardsChanged.RemoveListener(CardChangedHandler);
		}

	}
}
