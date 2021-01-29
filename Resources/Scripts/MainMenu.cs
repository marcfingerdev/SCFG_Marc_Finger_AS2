using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public InputField nameInputField;
    private bool NameEntered = false;

    // Start is called before the first frame update
    void Start()
    {
        nameInputField.characterLimit = 15;
        GameManager.timerIsRunning = false;
    }

    // Update is called once per frame
    void Update()
    {
        CheckIfNameEmpty();
        
    }

    void CheckIfNameEmpty()
    {
        if (nameInputField.text == "")
        {
            NameEntered = false;
        }
        else
        {
            NameEntered = true;
        }
    }

    public void PlayGame()
    {
        if (NameEntered == true)
        {
            GameManager.playerName = nameInputField.text;
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        }
    }

    public void QuitGame()
    {
        Application.Quit();
    }

}
