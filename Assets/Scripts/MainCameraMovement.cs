using UnityEngine;

public class MainCameraMovement : MonoBehaviour
{
    public Transform player;
    public Vector3 offset;
    
    void Update()
    {
        transform.position = new Vector3(player.position.x + offset.x, offset.y, offset.z); 
    }
}
