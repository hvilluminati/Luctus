using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
    
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
        SceneManager.LoadScene(prevSceneInd);
    }

    public void LoadStartScene()
    {
        SceneManager.LoadScene(0);
    }

    public void LoadTestScene()
    {
        SceneManager.LoadScene(4);
    }
}
