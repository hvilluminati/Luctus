using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
	public int sceneNumber;
	public Animator transition;
	public float transitionTime;

	public string sceneText = " ";

	public TMP_Text textDisplay;

	private void Start()
	{
		if (DataManager.instance.haveEntered[SceneManager.GetActiveScene().buildIndex])
		{
			sceneText = " ";
		}
		textDisplay.text = sceneText;
		DataManager.instance.haveEntered[SceneManager.GetActiveScene().buildIndex] = true;
	}

	public void LoadNextScene()
	{
		int currSceneInd = SceneManager.GetActiveScene().buildIndex;
		SceneManager.LoadScene(currSceneInd + 1);
	}

	public void LoadPrevScene()
	{
		int prevSceneInd = DataManager.instance.prevScene;
		DataManager.instance.SaveCoordinate(0, 0);

		//update the status of the enemy with the last collided ID
		int lastCollidedID = DataManager.instance.lastEnemyCollidedID;
		for (int i = 0; i < DataManager.instance.enemies.Length; i++)
		{
			if (DataManager.instance.enemies[i].enemyType.GetEnemyID() == lastCollidedID)
			{
				DataManager.EnemyState temp = DataManager.instance.enemies[i];
				temp.isAlive = false;
				DataManager.instance.enemies[i] = temp;
				break;
			}
		}
		SceneManager.LoadScene(prevSceneInd);
	}

	public void LoadStartScene()
	{
		SceneManager.LoadScene(0);
	}

	public void LoadIntroScene()
	{
		SceneManager.LoadScene(6);
	}

	public void LoadSelectedScene()
	{
		DeckManager.instance.GetComponent<DeckManager>().CreateNewDeck();
		DataManager.instance.gameOver = false;
		DataManager.instance.gameFinish = false;

		StartCoroutine(LoadLevel(sceneNumber));
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
