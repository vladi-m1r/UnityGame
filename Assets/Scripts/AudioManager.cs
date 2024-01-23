using System;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class AudioManager: MonoBehaviour
{

    // AudioSource group
    public AudioSource bgm;
    public AudioSource sfx;

    // UI Audio EFX
    [Header("UI SFX")]
    public AudioClip hoverButton;
    public AudioClip clickButton;

    // Sound efx game
    [Header("GAME SFX")]
    public AudioClip win;

    [Header("BGM GAME")]
    public AudioClip level1;
    public AudioClip level2;
    public AudioClip level3;
    public AudioClip level4;
    public AudioClip level5;
    public AudioClip lose;

    // Sound efx ship
    [Header("SHIP SFX")]
    public AudioClip death;
    public AudioClip collision;
    public AudioClip obtainedFuel;

    // Sound efx ship special treatment
    public AudioSource propulse;
    public AudioSource lowFuel;

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

}