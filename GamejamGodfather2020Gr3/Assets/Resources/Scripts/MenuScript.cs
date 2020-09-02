using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManager;

public class MenuScript : MonoBehaviour
{
   public void LaunchGame()
   {
       SceneManager.LoadScene(1);
   }

   public void DisplayOptions()
   {
       
   }

   public void QuitGame()
   {
       Application.QuitGame();
   }
}
