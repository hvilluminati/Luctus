using UnityEngine;
using System.Collections;

public class EnemyAttack : MonoBehaviour
{
	[SerializeField] private int charge;

	[SerializeField] private EnemyType enemy;

	[SerializeField] public Projectile projectile;
	[SerializeField] public Transform launchOffset;

	private void Start()
	{
		charge = 0;
	}


	// todo: integrate damage multiplier 

	public void AttackHandler(int damage)
	{

		if (enemy.GetIsBoss() && Random.value <= 0.75f)
		{
			// 75% chance of charging the attack
			ChargeAttack(damage);
		}

		else
        {
			MeleeAttack(damage);
			// placeholder for now.
			// 

        }

		Debug.Log($"Enemy of type is dealing damage");
	}

	private void MeleeAttack(int damage)
	{
		DataManager.instance.playerHealth -= damage;

	}


	
	private void RangedAttack()
	{
		projectile.Launch();
		Instantiate(projectile, launchOffset.position, transform.rotation);
	}
	

	private void ChargeAttack(int damage)
	{

		// TODO: if player does not block charge
		charge++;

		if (charge >= 3)
        {
			DataManager.instance.playerHealth -= damage;
        }

	}






}



