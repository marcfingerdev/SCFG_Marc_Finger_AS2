using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public float timeStart = 0;
    public bool timerIsRunning = false;

    public Text timerText;

    // Start is called before the first frame update
    void Start()
    {
        timerIsRunning = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (timerIsRunning)
        {
            timeStart += Time.deltaTime;
            timerText.text = timeStart.ToString("0.00");
        }
    }
}
