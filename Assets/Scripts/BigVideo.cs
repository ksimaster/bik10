using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Video;

public class BigVideo : MonoBehaviour
{
    private GameObject bigVideo;
    public float timeVideoBig = 10f;

    private void Start()
    {
        bigVideo = GameObject.FindGameObjectWithTag("Video");
        //bigImage.transform.GetChild(0).gameObject.GetComponent<Image>().enabled = true;
        //bigImage.transform.GetChild(0).gameObject.SetActive(false);
    }
    public void ToBigVideo()
    {
        PlayerPrefs.SetFloat("Timer", PlayerPrefs.GetFloat("Timer") - timeVideoBig);
        Debug.Log(PlayerPrefs.GetFloat("Timer"));
        bigVideo.SetActive(true);
        bigVideo.transform.GetChild(0).gameObject.SetActive(true);

        bigVideo.transform.GetChild(0).gameObject.GetComponent<VideoPlayer>().clip = gameObject.GetComponent<VideoPlayer>().clip;

    }
}
