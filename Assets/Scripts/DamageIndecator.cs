using TMPro;
using UnityEngine;

public class DamageIndecator : MonoBehaviour
{
	public Animator animator;
	public TMP_Text text;
	public void DisplayDamage(int amount)
	{
		text.text = amount.ToString();
		animator.Play("DisplayDamage");
	}
}
