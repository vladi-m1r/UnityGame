using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class scriptNiveles : MonoBehaviour
{
  public void loadLevel1()
  {
    SceneManager.LoadScene(4);
  }
  public void loadLevel2()
  {
    SceneManager.LoadScene(5);
  }
  public void loadLevel3()
  {
    SceneManager.LoadScene(6);
  }
  public void loadLevel4()
  {
    SceneManager.LoadScene(7);
  }
  public void loadLevel5()
  {
    SceneManager.LoadScene(8);
  }
  public void goBack()
  {
    SceneManager.LoadScene(0);
  }
}