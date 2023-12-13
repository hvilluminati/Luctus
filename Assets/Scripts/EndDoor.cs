using UnityEngine.SceneManagement;
using UnityEngine;

public class TheEnd : MonoBehaviour
{
    private bool enterAllowed;

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
            DataManager.instance.gameFinish = true;
            DataManager.instance.GetComponent<DataManager>().DataReset();
            SceneManager.LoadScene(0);
        }
    }
}
