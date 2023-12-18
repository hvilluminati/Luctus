using System.Collections;
using UnityEngine.SceneManagement;
using UnityEngine;

public class EnterGame : MonoBehaviour
{
    public int sceneNumber;
    public Animator transition;
    public float transitionTime;

    void Update()
    {
        if (Input.GetButtonDown("Submit"))
        {
            StartCoroutine(LoadLevel(sceneNumber));
        }
    }

    IEnumerator LoadLevel(int levelIndex)
    {
        //Play animation
        transition.SetTrigger("Start");

        //wait
        yield return new WaitForSeconds(transitionTime);

        //load scene
        SceneManager.LoadScene(levelIndex);
    }
}
