using UnityEngine;

public class EnemyMovement : MonoBehaviour
{

    public AnimationCurve myCurve;
    public int distance;

    void Update()
    {
        transform.position = new Vector3(myCurve.Evaluate((Time.time % myCurve.length)) + distance, transform.position.y, transform.position.z);
    }
}
