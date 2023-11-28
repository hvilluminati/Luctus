using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    public AnimationCurve myCurve;
    public int distance;
    private Vector3 initialPosition;

    private void Start()
    {
        // Store the initial position of the GameObject
        initialPosition = transform.position;

        if (!DataManager.instance.enemyAlive)
        {
            this.gameObject.SetActive(false);
        }
    }

    void Update()
    {
        // Update the position based on the animation curve and the initial position
        transform.position = new Vector3(initialPosition.x + myCurve.Evaluate((Time.time % myCurve.length)) * distance, initialPosition.y, initialPosition.z);
    }
}