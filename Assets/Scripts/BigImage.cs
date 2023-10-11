using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BigImage : MonoBehaviour
{
    private GameObject bigImage;

    private void Start()
    {
        bigImage = GameObject.FindGameObjectWithTag("Finish");
        //bigImage.transform.GetChild(0).gameObject.GetComponent<Image>().enabled = true;
        //bigImage.transform.GetChild(0).gameObject.SetActive(false);
    }
    public void ToBigImage()
    {
        bigImage.SetActive(true);
        bigImage.transform.GetChild(0).gameObject.SetActive(true);

        bigImage.transform.GetChild(0).gameObject.GetComponent<Image>().sprite = gameObject.GetComponent<Image>().sprite;
        
    }
}
