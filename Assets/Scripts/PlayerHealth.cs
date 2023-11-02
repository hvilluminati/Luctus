using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour
{
    public Image healthBar;
    public float healthAmount = 100;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (healthAmount <= 0)
        { Application.LoadLevel(Application.loadedLevel); }

        if (Input.GetKeyDown(KeyCode.Backspace)) { TakeDamage(30); }

        if (Input.GetKeyDown(KeyCode.Return)) { HealDamage(20); }
    }

    public void TakeDamage(float damage)
    {
        healthAmount -= damage;
        healthBar.fillAmount = healthAmount / 100f;
    }

    public void HealDamage(float healing)
    {
        if((healthAmount + healing) <= 100)
        {
            healthAmount += healing;
        }
        else
        {
            healthAmount = 100;
        }
        
        healthBar.fillAmount = Mathf.Clamp(healthAmount, 0, 100);

        healthBar.fillAmount = healthAmount / 100f;
    }
}
