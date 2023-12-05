using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyType : MonoBehaviour
{
    [Header("Enemy Settings")]
    [Tooltip("Unique identifier for the enemy")]
    [SerializeField] private int enemyID; // Enemy ID

    [Tooltip("The sprite for the enemy")]
    [SerializeField] private Sprite enemySprite; // Sprite for the enemy

    [Tooltip("Damage multiplier for the enemy")]
    [SerializeField] private float damageMultiplier = 1.0f; // Damage multiplier for the enemy

    [Tooltip("Check if the enemy is a boss")]
    [SerializeField] private bool isBoss = false; // Flag to check if the enemy is a boss

    [Tooltip("Health of the enemy")]
    [SerializeField] private int health = 100; // Health of the enemy

    // Additional properties can be added here as needed

    // Start is called before the first frame update
    void Start()
    {
        // Initialization code here
        CheckInitialAliveStatus();
    }
    private void Awake()
    {
        // Check for existing instance with the same ID
        EnemyType[] existingEnemies = FindObjectsOfType<EnemyType>();
        foreach (var enemy in existingEnemies)
        {
            if (enemy != this && enemy.GetEnemyID() == this.GetEnemyID())
            {
                // An enemy with the same ID already exists, handle accordingly
                Destroy(this.gameObject); // Option 1: Destroy this duplicate
                return; // Uncomment if destroying this object
                // -- OR --
                // UpdateExistingEnemy(enemy); // Option 2: Update existing enemy (define this method as needed)
            }
        }

        // Detach from parent to become a root GameObject
        transform.parent = null;
        DontDestroyOnLoad(gameObject);
    }

    private void Update()
    {
        CheckAliveStatus();
    }

    private void CheckAliveStatus()
    {
        // Assuming DataManager has a method to get the alive status of an enemy by ID
        bool isAlive = DataManager.instance.GetEnemyAliveStatus(enemyID);
        if (!isAlive)
        {
            gameObject.SetActive(false);
        }
    }

    private void LoadSprite()
    {
        // Code to load the sprite for the enemy
        var spriteRenderer = GetComponent<SpriteRenderer>();
        if (spriteRenderer != null)
        {
            spriteRenderer.sprite = enemySprite;
        }
    }

    private void CheckInitialAliveStatus()
    {
        // Check DataManager to see if this enemy should be active
        foreach (var enemyState in DataManager.instance.enemies)
        {
            if (enemyState.enemyType.GetEnemyID() == this.enemyID && !enemyState.isAlive)
            {
                this.gameObject.SetActive(false); // Deactivate the enemy if it's marked as dead in DataManager
                break;
            }
        }
    }

    // Getter for enemyID
    public int GetEnemyID()
    {
        return enemyID;
    }

    public Sprite GetSprite()
    {
        return enemySprite;
    }

    public int GetHealth()
    {
        return health;
    }

    // Additional methods can be added here, such as attack patterns, movement, etc.
}
