using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyType : MonoBehaviour
{
    [Tooltip("Must be unique to the scene")]
    [SerializeField] private int enemyID; 
    [SerializeField] private Sprite enemySprite; 
    [SerializeField] private float damageMultiplier = 1.0f; 
    [SerializeField] private bool isBoss = false; 
    [SerializeField] private int health = 100;
    [SerializeField] private Card card1;
    [SerializeField] private Card card2;
    [SerializeField] private Projectile projectile;

    void Start()
    {
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
                // An enemy with the same ID already exists, destroy the object
                Destroy(this.gameObject);
                return;
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
        bool isAlive = DataManager.instance.GetEnemyAliveStatus(enemyID);
        if (!isAlive)
        {
            gameObject.SetActive(false);
        }
    }

    //Not used currently
    private void LoadSprite()
    {
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

    public Card GetCard1()
    {
        return card1;
    }

    public Card GetCard2()
    {
        return card2;
    }

    public bool GetIsBoss()
    {
        return isBoss;
    }
}

