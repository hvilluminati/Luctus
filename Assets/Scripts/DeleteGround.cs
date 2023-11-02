using UnityEngine;

public class DeleteGround : MonoBehaviour
{
    public GameObject player;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            player.GetComponent<BoxCollider2D>().enabled = false;
        }
    }
}
