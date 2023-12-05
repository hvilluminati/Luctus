using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
    public int testSceneNumber;

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

        DeckManager.instance.AddRandomCard();
        SceneManager.LoadScene(prevSceneInd);
    }
    public void LoadStartScene()
    {
        SceneManager.LoadScene(0);
    }

    public void LoadTestScene()
    {
        DeckManager.instance.GetComponent<DeckManager>().CreateNewDeck();
        DataManager.instance.gameOver = false;
        DataManager.instance.gameFinish = false;

        SceneManager.LoadScene(testSceneNumber);
    }
}
