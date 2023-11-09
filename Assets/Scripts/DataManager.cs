using UnityEngine;

public class DataManager : MonoBehaviour
{
    public static DataManager instance;
    public float playerHealth;
    public float x_old;
    public float y_old;
    public float x_new;
    public float y_new;

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
}
