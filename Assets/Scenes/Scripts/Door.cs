using UnityEngine;
using UnityEngine.SceneManagement;

public class Door : MonoBehaviour
{
    [SerializeField] private int sceneNumber;
    private bool enterAllowed;
    public Transform player;
    public bool keyRequired;

    private void Start()
    {
        if (keyRequired)
        {
            this.enabled = false;
        }
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            enterAllowed = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            enterAllowed = false;
        }
    }

    private void Update()
    {
        if (Input.GetButtonDown("Submit") && enterAllowed)
        {
            DataManager.instance.SaveCoordinate(player.position.x, player.position.y);
            DataManager.instance.UpdatePrevScene(SceneManager.GetActiveScene().buildIndex);
            SceneManager.LoadScene(sceneNumber);
        }
    }
}