using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBirdZone : MonoBehaviour
{
    [SerializeField]
    public EnemyMovementBird bird;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            bird.track = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            bird.track = false;
        }
    }
}
