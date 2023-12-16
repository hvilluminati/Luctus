using Assets.Scripts;
using UnityEngine;

[CreateAssetMenu(fileName = "New Card", menuName = "Card")]
public class Card : ScriptableObject
{
	public new string name;

	[Multiline(4)]
	public string description;

	public Sprite artwork;

	[Space]
	public DamageType damageType;
	public int damage;
	public int shield;
	public int statusDuration;


	[Space]
	public CardEffect effect;
	public int effectAmount = 0;
}
