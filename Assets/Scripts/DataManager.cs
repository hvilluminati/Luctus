using System.Collections.Generic;
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

    public EnemyState[] enemies; // Array to keep track of enemies

    public bool gameOver = false;
    public bool gameFinish = false;

    public int lastEnemyCollidedID = -1; // ID of the last enemy collided with

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

    void Update()
    {
        InitializeEnemyStates();
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

    public void UpdatePrevScene(int sceneNumber)
    {
        prevScene = sceneNumber;
    }

    public void UpdateLastEnemyCollided(int enemyID)
    {
        lastEnemyCollidedID = enemyID;
    }

    public void DataReset()
    {
        playerHealth = 100;
        x_old = 0;
        y_old = 0;
        x_new = 0;
        y_new = 0;
        prevScene = 0;
        ResetEnemies();
        lastEnemyCollidedID = -1;
    }


    private void InitializeEnemyStates()
    {
        EnemyType[] enemyObjects = FindObjectsOfType<EnemyType>();

        foreach (var enemyObject in enemyObjects)
        {
            int enemyID = enemyObject.GetEnemyID();

            // Check if this enemy is already tracked
            bool found = false;
            foreach (var enemyState in enemies)
            {
                if (enemyState.enemyType.GetEnemyID() == enemyID)
                {
                    found = true;
                    break;
                }
            }

            // Add new enemy if it's not already tracked
            if (!found)
            {
                List<EnemyState> temp = new List<EnemyState>(enemies);
                temp.Add(new EnemyState
                {
                    enemyType = enemyObject,
                    isAlive = true
                });
                enemies = temp.ToArray();
            }
        }
    }

    public bool GetEnemyAliveStatus(int enemyID)
    {
        foreach (var enemyState in enemies)
        {
            if (enemyState.enemyType.GetEnemyID() == enemyID)
            {
                return enemyState.isAlive;
            }
        }
        return false; // Default to not alive if not found
    }

    private bool IsEnemyIDInList(List<EnemyState> enemyStates, int enemyID)
    {
        foreach (var enemyState in enemyStates)
        {
            if (enemyState.enemyType.GetEnemyID() == enemyID)
            {
                return true;
            }
        }
        return false;
    }


    // Define the EnemyState struct
    [System.Serializable]
    public struct EnemyState
    {
        public EnemyType enemyType; // Reference to the enemy type
        public bool isAlive; // Is the enemy alive?
    }

    // Method to reset all enemies to alive
    private void ResetEnemies()
    {
        if (enemies != null)
        {
            for (int i = 0; i < enemies.Length; i++)
            {
                enemies[i].isAlive = true;
            }
        }
    }

    // Add more methods as needed to manage enemies
}
