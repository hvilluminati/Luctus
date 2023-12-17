using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EnterBattle : MonoBehaviour
{
    public Animator transition;
    public float transitionTime;
    [SerializeField] private int sceneNumber;

    // Update is called once per frame
    public void StartBattle()
    {
        StartCoroutine(LoadLevel(sceneNumber));
    }

    IEnumerator LoadLevel(int levelIndex)
    {
        //Play animation
        transition.SetTrigger("Battle");

        Debug.Log("Is it the wait");
        //wait
        yield return new WaitForSeconds(transitionTime);

        //load scene
        Debug.Log("Before transition");
        SceneManager.LoadScene(levelIndex);
        Debug.Log("After transition");
    }
}
