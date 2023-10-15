using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PanelController : MonoBehaviour
{
    public GameObject panel;
    public float timerDuration;

    private float timer;
    private bool isTimerRunning;

    void Awake()
    {
        if (!PlayerPrefs.HasKey("Timer")) PlayerPrefs.SetFloat("Timer", timerDuration);
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
            timer = PlayerPrefs.GetFloat("Timer");
            timer -= Time.deltaTime;
            PlayerPrefs.SetFloat("Timer", timer);
            if (timer <= 0f)
            {
                panel.SetActive(true);
                timer = timerDuration;
                PlayerPrefs.SetFloat("Timer", timer);
                isTimerRunning = false;
            }
        }
        else
        {
            
            if (!panel.activeSelf)
            {
                //timer = timerDuration;
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



