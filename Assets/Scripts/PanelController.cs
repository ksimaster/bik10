using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PanelController : MonoBehaviour
{
    public GameObject panelCharge;
    public GameObject panelContact;

    public float timerDuration;

    private float timer;
    private bool isTimerRunning;
    private float beforeLoadTimer;

    void Awake()
    {
        if (!PlayerPrefs.HasKey("Timer")) PlayerPrefs.SetFloat("Timer", timerDuration);
        timer = timerDuration;
        isTimerRunning = true;
        beforeLoadTimer = PlayerPrefs.GetFloat("Timer");
    }

    void Update()
    {
        if (panelContact.activeSelf)
        {
            isTimerRunning = false;
            PlayerPrefs.SetFloat("Timer", beforeLoadTimer);
        }
        else if (panelCharge.activeSelf)
        {
            timer = timerDuration;
            isTimerRunning = false;
        }
        else
        {
            isTimerRunning = true;
            beforeLoadTimer = PlayerPrefs.GetFloat("Timer");
        }
        if (isTimerRunning)
        {
            timer = PlayerPrefs.GetFloat("Timer");
            timer -= Time.deltaTime;
            PlayerPrefs.SetFloat("Timer", timer);
            if (timer <= 0f)
            {
                panelCharge.SetActive(true);
                timer = timerDuration;
                PlayerPrefs.SetFloat("Timer", timer);
                isTimerRunning = false;
            }
        }
        else
        {
            
            if (!panelCharge.activeSelf)
            {
                //timer = timerDuration;
                isTimerRunning = true;
            }
            
        }
    }

    void OnDisable()
    {
        panelCharge.SetActive(false);
        timer = timerDuration;
        isTimerRunning = true;
    }

    
}



