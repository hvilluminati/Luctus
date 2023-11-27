using TMPro;
using UnityEngine;
using Assets.Scripts;

public class DeckCount : MonoBehaviour
{
	public TextMeshProUGUI cardCountField;

	public Deck deck;

	public void Start()
	{
		cardCountField.text = deck.Cards.Count.ToString();
	}
}
