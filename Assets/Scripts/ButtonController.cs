using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonController : MonoBehaviour
{
    public GameObject panel;
    public int clickCount;

    private int currentClickCount;
    public Slider slider;

    private void Start()
    {
        if (!PlayerPrefs.HasKey("maxCount")) PlayerPrefs.SetInt("maxCount", clickCount);
        if (!PlayerPrefs.HasKey("currentClickCount")) PlayerPrefs.SetInt("currentClickCount", 0);
        currentClickCount = PlayerPrefs.GetInt("currentClickCount");
        clickCount = PlayerPrefs.GetInt("maxCount");
        if (currentClickCount < clickCount && PlayerPrefs.GetFloat("Timer") < 20)
        {
            panel.SetActive(true);
            slider.maxValue = clickCount;
            slider.value = clickCount - currentClickCount;
        }
        
       // slider.maxValue = clickCount;
       // slider.value = clickCount - currentClickCount;
    }

    public void OnButtonClick()
    {
        currentClickCount++;
        PlayerPrefs.SetInt("currentClickCount", currentClickCount);
        slider.value = clickCount - currentClickCount;
        if (currentClickCount >= clickCount)
        {
            panel.SetActive(false);
            currentClickCount = 0;
            PlayerPrefs.SetInt("currentClickCount", currentClickCount);
            clickCount = PlayerPrefs.GetInt("maxCount");
            slider.value = clickCount;
        }
    }
}