using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class WinMenu : MonoBehaviour
{
    public Text WinText;
    public HighscoreTable highscoreTable;

    // Start is called before the first frame update
    void Start()
    {
        if (GameManager.timeStart != 0 && GameManager.playerName != "")
        {
            highscoreTable.AddHighscoreEntry((int)GameManager.timeStart, GameManager.playerName);
        }

        WinText.text = GameManager.playerName + ": " + GameManager.timeStart + " seconds";

        GameManager.timerIsRunning = false;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void MainMenu()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex -4);
    }

    public void QuitGame()
    {
        Application.Quit();
    }

}
