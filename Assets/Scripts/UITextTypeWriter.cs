using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class UITextTypeWriter : MonoBehaviour 
{

	public Text txt;
	string story;
    public string mainScene;
    float speed = 0.105f;

	void Awake () 
	{
		story = txt.text;
		txt.text = "";

		StartCoroutine ("PlayText");
	}

	IEnumerator PlayText()
	{
		foreach (char c in story) 
		{
			txt.text += c;
			yield return new WaitForSeconds (speed);
		}
	}

    public void PlayGame(){
        SceneManager.LoadScene(mainScene);
    }

}