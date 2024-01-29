using UnityEngine;
using UnityEngine.SceneManagement;

public class GameStateManager : MonoBehaviour
{

    public GameObject canvasCloseAnimation;

    public void gameOverInit()
    {
        this.closeAnimation();
    }

    private void closeAnimation()
    {
        this.canvasCloseAnimation.SetActive(true);
    }

    public void loadGameOverScene()
    {
        SceneManager.LoadScene(10);
    }
 
}