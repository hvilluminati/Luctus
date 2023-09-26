using UnityEngine;
using UnityEngine.UI;

public class CardDisplay : MonoBehaviour
{
	public Card card;

	public Text nameText;
	public Text descriptionText;

	public Image artworkImage;

	public Text damageText;
	public Text shieldText;

	void Start()
	{
		nameText.text = card.name;
		descriptionText.text = card.description;

		damageText.text = card.damage.ToString();
		shieldText.text = card.shield.ToString();
	}

}
