using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Menu : MonoBehaviour
{
    public AudioSource musicAudioSource;
    public Image SomOn;
    public Image SomOff;

    public void Jogar()
    {
        SceneManager.LoadScene("Jogo");
    }

    public void Sair()
    {
        Application.Quit();
    }

     public void ToggleMusic()
    {
        if (musicAudioSource.isPlaying)
        {
            musicAudioSource.Pause();
            SomOn.gameObject.SetActive(false);
            SomOff.gameObject.SetActive(true);
        }
        else
        {
            musicAudioSource.Play();
            SomOff.gameObject.SetActive(false);
            SomOn.gameObject.SetActive(true);
        }
    }
}