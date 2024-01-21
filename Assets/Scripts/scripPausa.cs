using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class scripPausa : MonoBehaviour
{

   public void ayuda(){
    SceneManager.LoadScene(1);
  }
   public void niveles(){
    SceneManager.LoadScene(2);
  }
  public void Salir(){
    Debug.Log("Salir...");
    Application.Quit();
  }
  public void continuar(){
    
  }
}
