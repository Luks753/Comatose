using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOverMenu : MonoBehaviour
{
    public static bool isPaused = false;
    public GameObject overMenuUI;
    public string mainScene;
    public string menuScene;
    public GameObject player;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if(!player.activeInHierarchy){
            Pause();
        } 
    }

    public void Quit(){
        Application.Quit();
    }

    public void Restart(){
        overMenuUI.SetActive(false);
        Time.timeScale = 1f;
        isPaused = false;
        SceneManager.LoadScene(mainScene);
        player.SetActive(true);
    }

    public void Menu(){
        Time.timeScale = 1f;
        SceneManager.LoadScene(menuScene);
    }

    void Pause(){
        overMenuUI.SetActive(true);
        Time.timeScale = 0f;
        isPaused = true;
    }
}
