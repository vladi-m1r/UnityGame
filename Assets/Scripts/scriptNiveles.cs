using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class scriptNiveles : MonoBehaviour
{
    public void nivel1(){
    SceneManager.LoadScene(4);
  }
   public void nivel2(){
    SceneManager.LoadScene(5);
  }
   public void nivel3(){
    SceneManager.LoadScene(6);
  }
   public void nivel4(){
    SceneManager.LoadScene(7);
  }
   public void nivel5(){
    SceneManager.LoadScene(8);
  }
  public void regresar(){
    SceneManager.LoadScene(0);
  }
}
