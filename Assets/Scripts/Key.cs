using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Key : MonoBehaviour
{
    public GameObject door;
    public GameObject invKey;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            door.GetComponent<Door>().enabled = true;

            this.gameObject.SetActive(false);
            invKey.SetActive(true);
        }
    }
}
