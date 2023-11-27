using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
    public int testSceneNumber;
    
    public void LoadNextScene()
    {
        int currSceneInd = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(currSceneInd+1);
    }

    public void LoadPrevScene()
    {
        int prevSceneInd = DataManager.instance.prevScene;
        DataManager.instance.SaveCoordinate(0, 0);
        DataManager.instance.enemyAlive = false;
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
