using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyType : MonoBehaviour
{

    [Tooltip("Must be unique to the scene")]
    [SerializeField] private int enemyID; 
    [SerializeField] private Sprite enemySprite;
    [SerializeField] private EnemyAttackType attackType;
    [SerializeField] private int damageMultiplier = 1;
    [SerializeField] private bool isBoss = false; 
    [SerializeField] private int health = 100;
    [SerializeField] private Card card1;
    [SerializeField] private Card card2;
    [SerializeField] private Projectile projectile;

    void Start()
    {
        CheckInitialAliveStatus();
        SetAttackType();
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
        SetAttackType();
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

    private void SetAttackType()
    {
        
        if (enemySprite.name.Contains("fire"))
        {
            attackType = EnemyAttackType.Fire;
        }
        else if (enemySprite.name.Contains("slime"))
        {
            attackType = EnemyAttackType.Slime;
        }
        else if (enemySprite.name.Contains("ice"))
        {
            attackType = EnemyAttackType.Ice;
        } else
        {
            attackType = EnemyAttackType.Melee;
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

    public bool GetIsBoss()
    {
        return isBoss;
    }


    public EnemyAttackType GetAttackType()
    {
        return attackType;
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
}

