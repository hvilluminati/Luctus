using UnityEngine;
using System.Collections;


public class EnemyAttack : MonoBehaviour
{
	public int charge;
	public Projectile projectile;
	private static int MELEE_DAMAGE = 20;
	private static int PROJECTILE_DAMAGE = 15;
	private static int CHARGED_DAMAGE = 50;
	private PlayerHealth playerHealth;

	private Enemy enemy;
	public Transform launchOffset;

    private void Start()
    {
		charge = 0;
    }




    // boss attacks = all the same amount of damage for simplicity
    // but they can use charged attacks :0
    public void AttackHandler(Enemy enemy)
    {

		/* 
		if (enemy.isBoss) {
			// chance of using charged attack
        }
		*/
		
		switch (enemy.type)
        {
			case EnemyType.Melee:
				MeleeAttack();
				break;
			case EnemyType.Ranged:
				RangedAttack();
				break;

        }

		
		Debug.Log($"Enemy of type {enemy.type} is dealing damage");
	}

	// effect on princess: hit animation 
	public void MeleeAttack() {
		playerHealth.TakeDamage(MELEE_DAMAGE);

	}

	// different type of ranged attacks include ice, fire, slime
	public void RangedAttack() {
		projectile.Launch();
		Instantiate(projectile, launchOffset.position, transform.rotation);
	}

	public void chargedAttack() {

		if (charge < 3)
        {
			charge++;
			
        } else
        {
			PlayerHealth.instance.TakeDamage(CHARGED_DAMAGE);
        }

	}




}
