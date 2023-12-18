using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour
{

	public Image healthBar;
	private float healthAmount = 100;
	private float damageTaken;
	public SpriteDamageEffect damageEffect;
	public GameObject player;
	public static PlayerHealth instance;
	private new AudioSource audio;

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
		{
			DataManager.instance.GetComponent<DataManager>().DataReset();
			DataManager.instance.gameOver = true;
			SceneManager.LoadScene(0);
		}

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
		Debug.Log("Took damage");
		audio.Play();
		healthAmount -= damage;
		DataManager.instance.ModifyHealth(-damage);
		healthBar.fillAmount = healthAmount / 100f;
		damageEffect.StartFlashRenderer();
		player.GetComponent<PlayerMovement>().Flinch();


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
}
