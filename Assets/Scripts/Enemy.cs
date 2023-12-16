using UnityEngine;

[CreateAssetMenu(fileName = "New Enemy", menuName = "Enemy")]
public class Enemy : ScriptableObject
{
	public int health;
	public Sprite spriteImage;
	public EnemyType type;
	public bool isBoss;
	public int charge;

    private void Awake()
    {
        InitializeEnemyFromLastCollided();
    }

    private void InitializeEnemyFromLastCollided()
    {
        int lastCollidedID = DataManager.instance.lastEnemyCollidedID;
        EnemyType lastCollidedEnemy = FindEnemyTypeById(lastCollidedID);

        if (lastCollidedEnemy != null)
        {
            type  = lastCollidedEnemy;
        }
    }

    private EnemyType FindEnemyTypeById(int enemyID)
    {
        foreach (var enemyState in DataManager.instance.enemies)
        {
            if (enemyState.enemyType.GetEnemyID() == enemyID)
            {
                return enemyState.enemyType;
            }
        }
        return null;
    }
}

