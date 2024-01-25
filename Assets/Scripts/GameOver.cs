using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOver : MonoBehaviour
{
    public void tryAgain()
    {
        int currentLevel = PlayerPrefs.GetInt("currentLevel");
        SceneManager.LoadScene(currentLevel);
    }

    public void goMenu()
    {
        SceneManager.LoadScene((int) Scenes.MENU);
    }

}
