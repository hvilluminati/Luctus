using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts
{
	[CreateAssetMenu(fileName = "New Deck", menuName = "Deck")]
	public class Deck : ScriptableObject
	{
		public List<Card> cards = new List<Card>();
	}
}
