using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
<<<<<<< HEAD
    public int testSceneNumber;
=======
>>>>>>> parent of 82bb07c (Merge pull request #31 from hvilluminati/feature/damage-animation)
    
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
<<<<<<< HEAD
        DeckManager.instance.AddRandomCard();
=======
>>>>>>> parent of 82bb07c (Merge pull request #31 from hvilluminati/feature/damage-animation)
        SceneManager.LoadScene(prevSceneInd);
    }

    public void LoadStartScene()
    {
        SceneManager.LoadScene(0);
    }

    public void LoadTestScene()
    {
<<<<<<< HEAD
        DeckManager.instance.GetComponent<DeckManager>().CreateNewDeck();
        DataManager.instance.gameOver = false;
        DataManager.instance.gameFinish = false;
        
        SceneManager.LoadScene(testSceneNumber);
=======
        SceneManager.LoadScene(4);
>>>>>>> parent of 82bb07c (Merge pull request #31 from hvilluminati/feature/damage-animation)
    }
}
