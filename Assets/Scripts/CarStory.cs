using UnityEngine;
using System.Collections;

public class CarStory : MonoBehaviour {

	public AudioSource ambience;
	public AudioSource radio;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		
	}


	public void playSound(string sound){

	}

	public void enterSchool(){
		Application.LoadLevel("scene1");
	}
}
