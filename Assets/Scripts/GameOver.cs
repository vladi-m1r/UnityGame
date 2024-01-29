using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOver : MonoBehaviour
{
    void Start()
    {
        AudioManager audioManager = AudioManager.audioManager;
        audioManager.bgm.Stop();
        audioManager.playAudio(audioManager.lose);
    }

    public void tryAgain()
    {
        int currentLevel = PlayerPrefs.GetInt("currentLevel") + 3;
        SceneManager.LoadScene(currentLevel);
    }

    public void goMenu()
    {
        SceneManager.LoadScene((int) Scenes.MENU);
    }

}
