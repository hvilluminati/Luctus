using UnityEngine;

public class DataManager : MonoBehaviour
{
    public static DataManager instance;
    public float playerHealth = 100;
    public float x_old = 0;
    public float y_old = 0;
    public float x_new = 0;
    public float y_new = 0;
    public int prevScene = 0;
    public bool enemyAlive = true;
    public bool gameOver = false;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject); // Keep the GameObject alive when switching scenes
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void ModifyHealth(float amount)
    {
        playerHealth += amount;
    }

    public void SaveCoordinate(float x_curr, float y_curr)
    {
        x_old = x_new;
        y_old = y_new;
        x_new = x_curr;
        y_new = y_curr;
    }

    public void UpdatePrevScene(int sceneNumer)
    {
        prevScene = sceneNumer;
    }
}
