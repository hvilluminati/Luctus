using UnityEngine;
using System.Collections;


public class EnemyAttack : MonoBehaviour
{
    public enum EnemyAttackType
    {
        Melee,
        Ranged
    }
    public int charge;
	public Projectile projectile;
	private static int MELEE_DAMAGE = 20;
	private static int PROJECTILE_DAMAGE = 15;
	private static int CHARGED_DAMAGE = 50;
	private PlayerHealth playerHealth;
	public Transform launchOffset;

    private void Start()
    {
		charge = 0;
    }

    // boss attacks = all the same amount of damage for simplicity
    // but they can use charged attacks :0
    public void AttackHandler(EnemyType enemyType)
    {
        switch (enemyType.GetAttackType())
        {
            case EnemyType.EnemyAttackType.Melee:
                MeleeAttack();
                break;
            case EnemyType.EnemyAttackType.Ranged:
                RangedAttack();
                break;
        }

        Debug.Log($"Enemy of type {enemyType.GetAttackType()} is dealing damage");
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
