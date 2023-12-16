using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    public float speed = 30f;
    private int damage;


    private void Start()
    {
        // pick image here using enemy type
        setDamage(damage);

    }

    void Update()
    {

        //transform.Translate(Vector2.left * speed * Time.deltaTime);
        transform.position += -transform.right * speed * Time.deltaTime;
    }

    public void Launch()
    {
        //GetComponent<Rigidbody>().velocity = transform.forward * speed;
    }



    private void OnTriggerEnter2D(Collider2D collision)
    {

        if (collision.tag == "Player")
        {
            Debug.Log("proejctile collided");
            //collision.GetComponent<PlayerHealth>().TakeDamage(damage);
            Destroy(this.gameObject);

        }


    }


    public void setDamage(int damage)
    {
        this.damage = damage;
    }
}