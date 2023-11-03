using UnityEngine;

public class DataManager : MonoBehaviour
{
    public static DataManager instance;
    public float playerHealth = 100;

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
}
