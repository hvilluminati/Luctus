using UnityEngine;
using UnityEngine.UI;

public class EnemyDecorator : MonoBehaviour
{
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
            image.sprite = collidedEnemy.GetSprite(); 
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
        return null;
    }
}