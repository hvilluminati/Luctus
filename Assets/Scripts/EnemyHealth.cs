using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
	public int health = 50;
	public SpriteDamageEffect damageEffect;

	// Start is called before the first frame update
	void Start()
	{
		damageEffect = GetComponent<SpriteDamageEffect>();
	}

	// Update is called once per frame
	void Update()
	{

		if (Input.GetKeyDown(KeyCode.Backspace)) { TakeDamage(20); }

	}

	public void TakeDamage(int damage)
	{
		health -= damage;

		damageEffect.StartFlashRenderer();

	}

}