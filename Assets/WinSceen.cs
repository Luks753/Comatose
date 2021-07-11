using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class WinSceen : MonoBehaviour
{
    public string menuScene;
    
    public void Menu(){
        SceneManager.LoadScene(menuScene);
    }

    public void Quit(){
        Application.Quit();
    }
}
