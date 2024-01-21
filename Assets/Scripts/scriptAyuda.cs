using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class scriptAyuda : MonoBehaviour
{
   public void regresar(){
    SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex - 1);
  }
   public void niveles(){
    SceneManager.LoadScene(2);
  }
}
