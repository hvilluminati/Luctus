using UnityEngine;

public class GameOver : MonoBehaviour
{
    public GameObject Panel;
    private void Start()
    {
        if (DataManager.instance.gameOver)
        {
            Panel.SetActive(true);
        }
    }
}
