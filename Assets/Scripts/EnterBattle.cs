using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EnterBattle : MonoBehaviour
{
	public Animator transition;
	public float transitionTime;
	[SerializeField] private int sceneNumber;

	public GameObject Player;

	// Update is called once per frame
	public void StartBattle()
	{
		StartCoroutine(LoadLevel(sceneNumber));
	}

	IEnumerator LoadLevel(int levelIndex)
	{
		//Play animation
		transition.SetTrigger("Battle");

		Player.GetComponent<PlayerMovement>().enabled = false;
		Player.GetComponent<Rigidbody2D>().velocity = Vector2.zero;

		//wait
		yield return new WaitForSeconds(transitionTime);

		//load scene
		SceneManager.LoadScene(levelIndex);

	}
}
