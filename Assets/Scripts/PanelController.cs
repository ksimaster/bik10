using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PanelController : MonoBehaviour
{
    public GameObject panel;
    public float timerDuration = 6f;

    private float timer;
    private bool isTimerRunning;

    void Start()
    {
        timer = timerDuration;
        isTimerRunning = true;
    }

    void Update()
    {
        if (panel.activeSelf) 
        {
            timer = timerDuration;
            isTimerRunning = false;
        } 
        if (isTimerRunning)
        {
            timer -= Time.deltaTime;
            if (timer <= 0f)
            {
                panel.SetActive(true);
                isTimerRunning = false;
            }
        }
        else
        {
            if (!panel.activeSelf)
            {
                timer = timerDuration;
                isTimerRunning = true;
            }
        }
    }

    void OnDisable()
    {
        panel.SetActive(false);
        timer = timerDuration;
        isTimerRunning = true;
    }
}



