using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PlayerHealth : MonoBehaviour
{
<<<<<<< HEAD

<<<<<<< HEAD

=======
>>>>>>> parent of 82bb07c (Merge pull request #31 from hvilluminati/feature/damage-animation)
    public Image healthBar;
    private float healthAmount = 100;
    private float damageTaken;

<<<<<<< HEAD
=======
	public Image healthBar;
	private float healthAmount = 100;
	private float damageTaken;
	public SpriteDamageEffect damageEffect;
	public GameObject player;
>>>>>>> e29620c59106179b577dae518312c3d74e01412a
=======
    // Start is called before the first frame update
    void Start()
    {
        damageTaken = 100 - DataManager.instance.playerHealth;
        UpdateDamage(damageTaken);
    }

    // Update is called once per frame
    void Update()
    {
        if (healthAmount <= 0)
        { SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex); }
>>>>>>> parent of 82bb07c (Merge pull request #31 from hvilluminati/feature/damage-animation)

        if (Input.GetKeyDown(KeyCode.Backspace)) { TakeDamage(30); }

        if (Input.GetKeyDown(KeyCode.Return)) { HealDamage(20); }
    }

    public void UpdateDamage(float damage)
    {
        healthAmount -= damage;
        healthBar.fillAmount = healthAmount / 100f;
    }

    public void TakeDamage(float damage)
    {
        healthAmount -= damage;
        DataManager.instance.ModifyHealth(-damage);
        healthBar.fillAmount = healthAmount / 100f;
    }

    public void HealDamage(float healing)
    {
        if((healthAmount + healing) <= 100)
        {
            healthAmount += healing;
            DataManager.instance.ModifyHealth(healing);
        }
        else
        {
            healthAmount = 100;
        }
        
        healthBar.fillAmount = Mathf.Clamp(healthAmount, 0, 100);

<<<<<<< HEAD
	public void TakeDamage(float damage)
	{
		healthAmount -= damage;
		DataManager.instance.ModifyHealth(-damage);
		healthBar.fillAmount = healthAmount / 100f;
<<<<<<< HEAD
        damageEffect.StartFlashRenderer();
        player.GetComponent<PlayerMovement>().Flinch();
		
		
=======
		player.GetComponent<PlayerMovement>().Flinch();
		damageEffect.StartFlashRenderer();

>>>>>>> e29620c59106179b577dae518312c3d74e01412a
	}


	public void HealDamage(float healing)
	{
		if ((healthAmount + healing) <= 100)
		{
			healthAmount += healing;
			DataManager.instance.ModifyHealth(healing);
		}
		else
		{
			healthAmount = 100;
		}

		healthBar.fillAmount = Mathf.Clamp(healthAmount, 0, 100);

		healthBar.fillAmount = healthAmount / 100f;
	}
=======
        healthBar.fillAmount = healthAmount / 100f;
    }
>>>>>>> parent of 82bb07c (Merge pull request #31 from hvilluminati/feature/damage-animation)
}
