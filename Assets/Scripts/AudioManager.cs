using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class AudioManager: MonoBehaviour
{

    // AudioSource group
    public AudioSource bgm;
    public AudioSource sfx;

    // UI Audio EFX
    [Header("UI EFX")]
    public AudioClip hoverButton;
    public AudioClip clickButton;

    // static instance to save the gameobject in loadscene
    public static AudioManager audioManager;

    void Awake()
    {
        if (!audioManager)
        {
            audioManager = this;
            DontDestroyOnLoad(this.gameObject);
        }
        else
        {
            Destroy(this.gameObject);
        }
    }

    public void playAudio(AudioClip audioClip)
    {
        this.sfx.PlayOneShot(audioClip);
    }

    public void nextSceneText()
    {
        SceneManager.LoadScene(10);
    }

}

