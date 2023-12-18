using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneTransition : MonoBehaviour
{
    public Transform player;
    public EnterBattle enterBattle;


    private void Start()
    {
        if (player == null)
        {
            player = GameObject.FindGameObjectWithTag("Player").transform;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            Transform playerTransform = collision.gameObject.transform;
            DataManager.instance.SaveCoordinate(playerTransform.position.x, playerTransform.position.y);
            DataManager.instance.UpdatePrevScene(SceneManager.GetActiveScene().buildIndex);

            // Get the EnemyType component from this enemy
            EnemyType enemy = GetComponent<EnemyType>();
            if (enemy != null)
            {
                // Update the lastEnemyCollidedID in DataManager
                int enemyID = enemy.GetEnemyID();
                DataManager.instance.UpdateLastEnemyCollided(enemyID);
                SetEnemyAliveStatusInDataManager(enemyID, false);
            }

            DisableOverworldEnemies();
            enterBattle.StartBattle();
        }
    }
    private void SetEnemyAliveStatusInDataManager(int enemyID, bool isAlive)
    {
        for (int i = 0; i < DataManager.instance.enemies.Length; i++)
        {
            if (DataManager.instance.enemies[i].enemyType.GetEnemyID() == enemyID)
            {
                DataManager.EnemyState updatedState = DataManager.instance.enemies[i];
                updatedState.isAlive = isAlive;
                DataManager.instance.enemies[i] = updatedState;
                break;
            }
        }
    }

    private void DisableOverworldEnemies()
    {
        GameObject[] overworldEnemies = GameObject.FindGameObjectsWithTag("OverworldEnemy");
        foreach (GameObject enemy in overworldEnemies)
        {
            enemy.SetActive(false); // Disable each overworld enemy
        }
    }
}
