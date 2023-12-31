using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrapDamage : MonoBehaviour
{
    [SerializeField] private float damage;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            //DataManager.instance.ModifyHealth(-damage);
            collision.GetComponent<PlayerHealth>().TakeDamage(damage);
        }
    }
}
