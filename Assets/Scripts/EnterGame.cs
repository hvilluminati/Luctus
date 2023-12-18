using System.Collections;
using UnityEngine.SceneManagement;
using UnityEngine;

public class EnterGame : MonoBehaviour
{
    public int sceneNumber;
    public Animator Transition;
    public float TransitionTime;

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
        Transition.SetTrigger("Start");

        //wait
        yield return new WaitForSeconds(TransitionTime);

        //load scene
        SceneManager.LoadScene(levelIndex);
    }
}
