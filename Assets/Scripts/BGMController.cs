using UnityEngine;

public class BGMController : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        int currentLevel = PlayerPrefs.GetInt("currentLevel", -1);

        AudioManager audioManager = AudioManager.audioManager;

        audioManager.bgm.Stop();

        switch (currentLevel) {
            case 1: 
                audioManager.bgm.clip = audioManager.level1;
                break;
            case 2:
                audioManager.bgm.clip = audioManager.level2;
                break;
            case 3:
                audioManager.bgm.clip = audioManager.level3;
                break;
            case 4:
                audioManager.bgm.clip = audioManager.level4;
                break;
            case 5:
                audioManager.bgm.clip = audioManager.level5;
                break;
        }

        audioManager.bgm.Play();
    }
}