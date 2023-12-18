using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class EnemyAttack : MonoBehaviour
{
	private int charge = 0;
	private int damage;
	private static int DEFAULT_DAMAGE = 5;
    public PlayerHealth playerHealth;


    [SerializeField] public Projectile projectile;
	[SerializeField] public Transform launchOffset;


	void Update()
	{
		// Check for spacebar input to launch the projectile
		if (Input.GetKeyDown(KeyCode.Space))
		{
			projectile.Launch();
		}
	}

	// todo: integrate damage multiplier 

	public void AttackHandler(EnemyAttackType attackType, bool isBoss)
	{

		Debug.Log("hey");

		this.damage = (int) attackType * DEFAULT_DAMAGE;

		

		if (isBoss && Random.value <= 0.75f)
		{
			// boss has 75% chance of charging the attack
			
			ChargeAttack();
		}

		else
        {
			// placeholder to test battle scene
			MeleeAttack();
        }

	}

	private void MeleeAttack()
	{

		playerHealth.TakeDamage(damage);

	}


	
	private void RangedAttack()
	{
		projectile.setDamage(damage);
		Instantiate(projectile, launchOffset.position, transform.rotation);
	}
	

	private void ChargeAttack()
	{

		// TODO: if player does not block charge
		charge++;

		if (charge >= 3)
        {
			playerHealth.TakeDamage(damage);
		}

	}






}



