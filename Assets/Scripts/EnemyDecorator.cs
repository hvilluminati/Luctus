using UnityEngine;
using UnityEngine.UI;

public class EnemyDecorator : MonoBehaviour
{
	public Enemy enemy;
	public Sprite spriteImage;
	public Image image;
    public EnemyType collidedEnemy;


    private void Start()
    {
        SetImageToLastCollidedEnemySprite();
    }

    private void SetImageToLastCollidedEnemySprite()
    {
        int lastCollidedID = DataManager.instance.lastEnemyCollidedID;
        collidedEnemy = GetEnemyTypeFromDataManager(lastCollidedID);

        if (collidedEnemy != null)
        {
            image.sprite = collidedEnemy.GetSprite(); // Assuming EnemyType has a GetSprite method
        }
    }

    private EnemyType GetEnemyTypeFromDataManager(int enemyID)
    {
        foreach (var enemyState in DataManager.instance.enemies)
        {
            if (enemyState.enemyType.GetEnemyID() == enemyID)
            {
                return enemyState.enemyType;
            }
        }
        return null; // Return null if no matching enemy is found
    }
}