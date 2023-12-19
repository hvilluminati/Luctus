using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EnterBattle : MonoBehaviour
{
    public Animator transition;
    public float transitionTime;
    [SerializeField] private int sceneNumber;
    public AudioSource audio;
    public GameObject Player;

    // Update is called once per frame

    private void Start()
    {
        // Ensure the AudioSource is disabled at the start
        if (audio != null)
        {
            audio.enabled = false;
        }
    }

    public void StartBattle()
    {
        Player.GetComponent<PlayerMovement>().enabled = false;
        Player.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
        StartCoroutine(LoadLevel(sceneNumber));
    }

    IEnumerator LoadLevel(int levelIndex)
    {
        //Play animation
        transition.SetTrigger("Battle");
        if (audio != null)
        {
            audio.enabled = true;
        }

        //wait
        yield return new WaitForSeconds(transitionTime);

        //load scene
        SceneManager.LoadScene(levelIndex);

    }
}
