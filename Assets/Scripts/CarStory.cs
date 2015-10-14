using UnityEngine;
using System.Collections;

public class CarStory : MonoBehaviour {

	public AudioSource ambience;
	public AudioSource radio;

	public ScreenFade ScreenFader;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		
	}


	public void playSound(string sound){

	}

	public void enterSchool(){
		Debug.Log("Enterschool");
		ScreenFader.FadeToBlack();
		Application.LoadLevelAsync("scene1");
		//Application.LoadLevel("scene1");
	}
}
