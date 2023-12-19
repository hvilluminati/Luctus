using UnityEngine;
using UnityEngine.SceneManagement;

public class ObjectActivator : MonoBehaviour
{
    private DataManager dataManager;
    private DeckManager deckManager;
    public int sceneNumber;
    public PlayerHealth playerHealth;
    public Transform playerTransform;
    public float activationRange = 5f;
    public GameObject flame;
    public GameObject glow;
    public GameObject spark;

    void Start()
    {
        dataManager = DataManager.instance;
        deckManager = DeckManager.instance;
        flame.SetActive(false);
        glow.SetActive(false);
        spark.SetActive(false);
    }

    void Update()
    {
        if (CanActivateObject())
        {
            flame.SetActive(true);
            spark.SetActive(true);
            glow.SetActive(true);

            if (IsPlayerInRange() && CanActivateObject() && Input.GetKeyDown(KeyCode.Return)
            {
                Debug.Log("Bonfire activated");
                ActivateBonfire();
            }

        }
    }

    private bool IsPlayerInRange()
    {
        return Vector3.Distance(transform.position, playerTransform.position) <= activationRange;
    }


    private bool CanActivateObject()
    {
        foreach (var enemyState in dataManager.enemies)
        {
            if (enemyState.enemyType.GetIsBoss() && enemyState.isAlive)
            {
                return false;
            }
        }
        return true;
    }
    private void ActivateBonfire()
    {
        playerHealth.HealDamage(100);
        // Find all objects with EnemyType component
        EnemyType[] enemies = FindObjectsOfType<EnemyType>();

        foreach (EnemyType enemy in enemies)
        {
            Destroy(enemy.gameObject); // Destroy the GameObject to which the EnemyType component is attached
        }

        if (DataManager.instance != null)
        {
            DataManager.instance.DataReset();
        }
        if (deckManager != null)
        {
            DeckManager.instance.SetCurrentDeckToCollectedDeck();
        }

        SceneManager.LoadScene(sceneNumber);
    }
}

