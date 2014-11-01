using UnityEngine;
using System.Collections;

public class StoryHandler : MonoBehaviour {

	public const int EVENT_ENTER = 0;
	public const int EVENT_EXIT = 1; // EXIT NOT IMPLEMENTED
	public const int EVENT_NORTH = 2;
	public const int EVENT_SOUTH = 3;
	public const int EVENT_EAST = 4;
	public const int EVENT_WEST = 5;

	private Player player;

	// Use this for initialization
	void Start () {
		player = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Player>() as Player;
		if(player == null) Debug.Log("ERROR: NO PLAYER");
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void handleUseEvent(string tileName, int direction){
		// "EXAMPLE CODE"
		if(tileName == ""){
			switch(direction){			
			case EVENT_NORTH:
				break;
			case EVENT_SOUTH:
				break;
			case EVENT_EAST:
				break;
			case EVENT_WEST:
				break;
			}
		}
	}

	public void handleMoveEvent(string tileName, int eventType){
		// "EXAMPLE CODE"
		if(tileName == ""){
			switch(eventType){
			case EVENT_ENTER:
				break;
			case EVENT_EXIT:
				break;
			case EVENT_NORTH:
				break;
			case EVENT_SOUTH:
				break;
			case EVENT_EAST:
				break;
			case EVENT_WEST:
				break;
			}
		}

		if(tileName == "Start"){
			switch(eventType){
			case EVENT_ENTER:
				twineDisplay("enter school");
				break;
			case EVENT_EXIT:
				break;
			case EVENT_NORTH:
				break;
			case EVENT_SOUTH:
				break;
			case EVENT_EAST:
				break;
			case EVENT_WEST:
				break;
			}
		}

		if(tileName == "Office"){
			switch(eventType){
			case EVENT_ENTER:
				twineDisplay("office");
				break;
			case EVENT_EXIT:
				break;
			case EVENT_NORTH:
				break;
			case EVENT_SOUTH:
				break;
			case EVENT_EAST:
				break;
			case EVENT_WEST:
				break;
			}
		}

		if(tileName == "Library"){
			switch(eventType){
			case EVENT_ENTER:
				twineDisplay("library");
				break;
			case EVENT_EXIT:
				break;
			case EVENT_NORTH:
				break;
			case EVENT_SOUTH:
				break;
			case EVENT_EAST:
				break;
			case EVENT_WEST:
				break;
			}
		}

		if(tileName == "Basement"){
			switch(eventType){
			case EVENT_ENTER:
				twineDisplay("basement");
				break;
			case EVENT_EXIT:
				break;
			case EVENT_NORTH:
				break;
			case EVENT_SOUTH:
				break;
			case EVENT_EAST:
				break;
			case EVENT_WEST:
				break;
			}
		}
	}

	private void twineDisplay(string passageName){
		Application.ExternalCall("TwineDisplay", passageName);
	}

	public void stopMovement(){
		player.allowMovement = false;
	}
	
	public void startMovement(){
		player.allowMovement = true;
	}
}
