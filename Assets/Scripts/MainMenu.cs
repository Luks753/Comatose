using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public string mainScene;

    public void PlayGame(){
        SceneManager.LoadScene(mainScene);
    }

    public void Quit(){
        Application.Quit();
    }
}
