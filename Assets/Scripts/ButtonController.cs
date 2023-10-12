using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonController : MonoBehaviour
{
    public GameObject panel;
    public int clickCount = 10;

    private int currentClickCount;
    public Slider slider;

    void Start()
    {
        currentClickCount = 0;
        slider.maxValue = clickCount;
        slider.value = clickCount - currentClickCount;
    }

    public void OnButtonClick()
    {
        currentClickCount++;
        slider.value = clickCount - currentClickCount;
        if (currentClickCount >= clickCount)
        {
            panel.SetActive(false);
            currentClickCount = 0;
            slider.value = clickCount;
        }
    }
}