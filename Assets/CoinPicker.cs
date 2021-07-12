using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CoinPicker : MonoBehaviour
{
    public GameObject[] empty;
    public GameObject[] keypart;

    private int counter = 0;
    private string namee;
    public AudioSource audioKey;

    private void OnTriggerEnter2D(Collider2D other){
        if(other.transform.tag == "Key"){
            namee = other.gameObject.name;
            Destroy(other.gameObject);
            //play pickup key sound
            audioKey.PlayOneShot(audioKey.clip);
            hudChanger(namee);
        }
    }

    void hudChanger(string namee){
        switch(namee)
        {
            case "Feather1":
                keypart[0].SetActive(true);
                empty[0].SetActive(false);
                counter++;
                break;
            case "Feather2":
                keypart[1].SetActive(true);
                empty[1].SetActive(false);
                counter++;
                break;
            case "Key3":
                keypart[2].SetActive(true);
                empty[2].SetActive(false);
                counter++;
                break;
            case "Filter4":
                keypart[3].SetActive(true);
                empty[3].SetActive(false);
                counter++;
                break;
            default:
                break;
        }

        if(counter == 4)
            Win();
    }

    void Win(){
        SceneManager.LoadScene("WinScreen");
    }
}
