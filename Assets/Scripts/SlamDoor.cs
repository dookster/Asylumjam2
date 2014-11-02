using UnityEngine;
using System.Collections;

public class SlamDoor : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		if(Input.GetButtonUp("Use")){
			//slamDoor();
		}
	}

	public void slamDoor(){
		iTween.RotateAdd(gameObject, iTween.Hash("y", -110, "time", 0.3f, "easetype", "easeInQuad"));
		AudioSource audioSource =  GetComponent<AudioSource>() as AudioSource;
		audioSource.PlayDelayed(0.1f);
	}
}
