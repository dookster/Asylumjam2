using UnityEngine;
using System.Collections;

public class SoundHandler : MonoBehaviour {

	public AudioClip openDoor;
	public AudioClip rufflePapers;
	public AudioClip lockedDoor;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void playOpenDoor(){
		AudioSource.PlayClipAtPoint(openDoor, transform.position);
	}

}
