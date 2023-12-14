using UnityEngine;

public class EnemyMovement : MonoBehaviour
{

    public AnimationCurve myCurve;
    public int distance;

    private void Start()
    {
<<<<<<< HEAD
        // Store the initial position of the GameObject
        initialPosition = transform.position;

=======
>>>>>>> parent of 82bb07c (Merge pull request #31 from hvilluminati/feature/damage-animation)
        if (!DataManager.instance.enemyAlive)
        {
            this.gameObject.SetActive(false);
        }
    }
    void Update()
    {
        transform.position = new Vector3(myCurve.Evaluate((Time.time % myCurve.length)) + distance, transform.position.y, transform.position.z);
    }
}
