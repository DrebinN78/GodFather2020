using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class MenuScript : MonoBehaviour
{
   public void LaunchGame()
   {
       SceneManager.LoadScene(1);
   }

   public void QuitGame()
   {
       Application.Quit();
   }

   public void LauchTuto()
   {
       SceneManager.LoadScene(2);
   }
}
