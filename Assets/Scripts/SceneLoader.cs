using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
    
    public void LoadNextScene()
    {
        int currSceneInd = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(currSceneInd+1);
    }

    public void LoadStartScene()
    {
        SceneManager.LoadScene(0);
    }

}
