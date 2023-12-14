using UnityEngine;
using System.Collections;


public class EnemyAttack : MonoBehaviour
{
	private int damage;
	private int charge;
	private Projectile projectile;
	private static int MELEE_DAMAGE = 20;
	private static int PROJECTILE_DAMAGE = 15;
	private static int CHARGED_DAMAGE = 50;
	private PlayerHealth playerHealth;

    private void Start()
    {
		damage = 0;
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
		PlayerHealth.instance.TakeDamage(damage);
	}

	// effect on princess: hit animation 
	public void MeleeAttack() {
<<<<<<< HEAD
		playerHealth.TakeDamage(MELEE_DAMAGE);
=======
		damage = 20;
>>>>>>> parent of 0e91204 (Merge branch 'development' of https://github.com/hvilluminati/Luctus into feature/damage-animation)

	}

	// different type of ranged attacks include ice, fire, slime
	public void RangedAttack() {
<<<<<<< HEAD
=======
		damage = 15;
		projectile.setDamage(damage);
>>>>>>> parent of 0e91204 (Merge branch 'development' of https://github.com/hvilluminati/Luctus into feature/damage-animation)
		projectile.Launch();
	}

	public void chargedAttack() {

		if (charge < 3)
        {
			charge++;
			damage = 0;
        } else
        {
			damage = 50; 
        }

	}




}
