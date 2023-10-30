using UnityEngine;
using UnityEngine.SceneManagement;

public class Door : MonoBehaviour
{
    [SerializeField] private int sceneNumber;
    private bool enterAllowed;

    private void Start()
    {
        this.enabled = false;
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
            SceneManager.LoadScene(sceneNumber);
        }
    }
}