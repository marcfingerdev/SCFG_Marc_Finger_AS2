using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class WinMenu : MonoBehaviour
{
    public Text WinText;

    // Start is called before the first frame update
    void Start()
    {
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
