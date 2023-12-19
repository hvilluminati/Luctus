using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovementBird : MonoBehaviour
{
    public float velocity;
    public bool track = false;
    public Transform startingPoint;
    private GameObject player;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
    }


    void Update()
    {
        if(track)
        {
            Track();
            Flip();
        }
        else if (transform.position != null)
        {
            Return();
        }
    }

    private void Return()
    {
        transform.position = Vector2.MoveTowards(transform.position, startingPoint.position, velocity * Time.deltaTime);
    }
    private void Track()
    {
        transform.position = Vector2.MoveTowards(transform.position, player.transform.position, velocity * Time.deltaTime);
    }

    private void Flip()
    {
        if (transform.position.x > player.transform.position.x) 
        {
            transform.rotation = Quaternion.Euler(0, 180, 0);
        }
        else
        {
            transform.rotation = Quaternion.Euler(0, 0, 0);
        }
    }

    private void OnBecameVisible()
    {
        enabled = true;
    }

    private void OnBecameInvisible()
    {
        enabled = false;
    }

}
