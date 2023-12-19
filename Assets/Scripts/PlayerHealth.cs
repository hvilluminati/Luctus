using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public class PlayerHealth : MonoBehaviour
{

	public Image healthBar;
	private float healthAmount = 100;
	private float damageTaken;
	public SpriteDamageEffect damageEffect;
	public GameObject player;
	public static PlayerHealth instance;
	public TMP_Text healthNumber;
	public new AudioSource audio;

	// Start is called before the first frame update
	void Start()
	{
		damageTaken = 100 - DataManager.instance.playerHealth;
		UpdateDamage(damageTaken);
	}

	// Update is called once per frame
	void Update()
	{
		healthNumber.text = healthAmount.ToString();
		if (healthAmount <= 0)
		{
			DataManager.instance.GetComponent<DataManager>().DataReset();
			DataManager.instance.gameOver = true;
			SceneManager.LoadScene(0);
		}
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

		Debug.Log("healthbar decreased");
		
        damageEffect.StartFlashRenderer();

		Debug.Log("flash started");
		if (player.GetComponent<PlayerMovement>() != null)
        {
			player.GetComponent<PlayerMovement>().Flinch();
		}
		
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
