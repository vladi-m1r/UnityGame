using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ButtonSoundTrigger : MonoBehaviour
{

    private AudioManager audioManager;

    void Awake()
    {
        // Audio for click
        this.audioManager = AudioManager.audioManager;
        GetComponent<Button>().onClick.AddListener(() => this.audioManager.playAudio(this.audioManager.clickButton));

        // Audio for hover button
        EventTrigger eventTrigger = GetComponent<EventTrigger>();
        // Get the pointer enter entry and set audio to play
        eventTrigger.triggers[0].callback.AddListener((BaseEventData e) => this.audioManager.playAudio(this.audioManager.hoverButton));
    }

}