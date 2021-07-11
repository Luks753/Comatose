using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    public static bool isPaused = false;
    public GameObject pauseMenuUI;
    public string mainScene;

    private AudioSource bgAudio;
    public Camera dCamera;
    // Start is called before the first frame update
    void Start()
    {
        bgAudio = dCamera.GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape) && Time.timeScale == 1f){
            if(isPaused){
                Resume();
            } else{
                Pause();
            }
        }        
    }

    public void Resume(){
        bgAudio.Play();
        pauseMenuUI.SetActive(false);
        Time.timeScale = 1f;
        isPaused = false;
    }

    public void Quit(){
        Application.Quit();
    }

    public void Restart(){
        pauseMenuUI.SetActive(false);
        Time.timeScale = 1f;
        isPaused = false;
        SceneManager.LoadScene(mainScene);
    }

    void Pause(){
        //pauses bg audio
        bgAudio.Pause();

        pauseMenuUI.SetActive(true);
        Time.timeScale = 0f;
        isPaused = true;
    }
}
