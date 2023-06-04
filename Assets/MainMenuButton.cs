using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
   public void PlayGame()
   {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex +1);
   }
   public void Exit()
   {
   Application.Quit();
   }      
   //tombol play
    public void PlayGame (string Level1) 
    {
        SceneManager.LoadScene(Level1);
        Debug.Log("Ini Scene Level 1 Aktif" + Level1);
    }

    public void PlayLevel2 (string Level2) {
        SceneManager.LoadScene(Level2);
        Debug.Log("Ini Scene Level 2 Aktif" + Level2);
    }

    public void PlayLevel3 (string Level3) {
        SceneManager.LoadScene(Level3);
        Debug.Log("Ini Scene Level 3 Aktif" + Level3);
    }
}