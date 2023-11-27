using UnityEngine;

public class GameFinished : MonoBehaviour
{
    public GameObject Panel;
    private void Start()
    {
        if (DataManager.instance.gameFinish)
        {
            Panel.SetActive(true);
        }
    }
}
