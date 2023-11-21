using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CardDecorator : MonoBehaviour
{
	public Card card;

	public TMP_Text titleTextField;
	public TMP_Text descriptionTextField;

	public Image artworkImage;

	public TMP_Text damageTextField;
	public TMP_Text shieldTextField;

	public void Instantiate()
	{
		titleTextField.text = card.name;
		descriptionTextField.text = card.description;
		artworkImage.sprite = card.artwork;

		damageTextField.text = card.damage.ToString();
		shieldTextField.text = card.shield.ToString();
	}

}
