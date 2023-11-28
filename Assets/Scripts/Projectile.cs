using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    public float speed = 30f;
    private int damage;

    void Update()
    {
       
        transform.Translate(Vector2.left * speed * Time.deltaTime);
    }

    public void Launch()
    {
        GetComponent<Rigidbody>().velocity = transform.forward * speed;

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            
            collision.GetComponent<PlayerHealth>().TakeDamage(damage);
        }
    }


    public void setDamage(int damage)
    {
        this.damage = damage;
    }
}
