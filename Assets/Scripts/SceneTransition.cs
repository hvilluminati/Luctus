using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneTransition : MonoBehaviour
{
    [SerializeField] private int sceneNumber;
    public Transform player;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            DataManager.instance.SaveCoordinate(player.position.x, player.position.y);
            DataManager.instance.UpdatePrevScene(SceneManager.GetActiveScene().buildIndex);
            SceneManager.LoadScene(sceneNumber);
        }
    }
}
