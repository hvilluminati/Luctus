using UnityEngine;

[CreateAssetMenu(fileName = "New Enemy", menuName = "Enemy")]
public class Enemy : ScriptableObject
{
	public int health;
	public Sprite spriteImage;
	public EnemyType type;
	public bool isBoss;
	public int charge;

}
