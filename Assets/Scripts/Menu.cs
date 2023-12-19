using UnityEngine.SceneManagement;
using UnityEngine;
using TMPro;


public class Menu : MonoBehaviour
{
    public GameObject MenuPanel;
    public TMP_Text audioText;
    public GameObject MusicPlayer;
    private AudioSource GameMusic;

    void Start()
    {
        MenuPanel.SetActive(false);
        GameMusic = MusicPlayer.GetComponent<AudioSource>();
        if (GameMusic.isPlaying)
        {
            audioText.text = "Turn Off Music";
        }
        else
        {
            audioText.text = "Turn on Music";
        }
    }

    // Update is called once per frame
    void Update()
    {
    }

    public void OpenMenu()
    {
        MenuPanel.SetActive(true);
        Time.timeScale = 0;
    }

    public void ExitGame()
    {
        //DataManager.instance.gameOver = true;
        //DataManager.instance.GetComponent<DataManager>().DataReset();
        //SceneManager.LoadScene(0);
        DataManager.instance.gameOver = true;
        DataManager.instance.GetComponent<DataManager>().DataReset();

        // Quit the application
        Application.Quit();

    }

    public void ToggleMusic()
    {
        if (GameMusic.isPlaying)
        {
            GameMusic.Pause();
            audioText.text = "Turn on Music";
        }
        else
        {
            GameMusic.Play(0);
            audioText.text = "Turn Off Music";
        }
    }

    public void CloseMenu()
    {
        MenuPanel.SetActive(false);
        Time.timeScale = 1;
    }
}
