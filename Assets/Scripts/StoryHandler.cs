using UnityEngine;
using System.Collections;

public class StoryHandler : MonoBehaviour {

	public const int EVENT_ENTER = 0;
	public const int EVENT_EXIT = 1; // EXIT NOT IMPLEMENTED
	public const int EVENT_NORTH = 2;
	public const int EVENT_SOUTH = 3;
	public const int EVENT_EAST = 4;
	public const int EVENT_WEST = 5;

	// Quest constants, for keeping track of where we are in the story
	//public const int Q_FIND_OFFICE 							= 0;
	//public const int Q_FIND_LIBRARY 						= 20;
	//public const int Q_NIGHTMARE_VISIT_ALL_CLASSROOMS 		= 40;
	//public const int Q_REALITY_RETURN_BASEMENT_DOOR 		= 60;
	//public const int Q_GET_KEY_FROM_LOUNGE_CLOSET	 		= 80;
	//public const int Q_UNLOCK_BASEMENT				 	= 90;

	public const int Q_FIND_PAPERS							= 10;
	public const int Q_SEARCH_FOR_FAMILY_NAME				= 30; // read diary/essay
	public const int Q_NIGHTMARE_FIND_BASEMENT_DOOR 		= 50;
	public const int Q_SEARCH_LIBRARY_FOR_CLUE_AND_FIND_KEY = 70;
	public const int Q_ENTER_BASEMENT				 		= 100;
	public const int Q_FIND_RELIGIOUS_TEXTS			 		= 110;
	public const int Q_LEAVE						 		= 120;

	public int currentQuest = -1;

	public GameObject monoliths;
	public GameObject knocking;

	public AudioClip normalAmbience;
	public AudioClip nightmareAmbience;

	private AudioSource audioSource;
	private Player player;
	private SoundHandler soundHandler;

	// Use this for initialization
	void Start () {
		player = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Player>() as Player;
		if(player == null) Debug.Log("ERROR: NO PLAYER");
		audioSource = GetComponent<AudioSource>() as AudioSource;
		soundHandler = GameObject.Find("SoundHandler").GetComponent<SoundHandler>() as SoundHandler;
	}
	
	// Update is called once per frame
	void Update () {
		if(Input.GetButtonUp("Use")){
			//showNightmareWorld();
		}
	}

	/***********************************************************
	 * 
	 * USE
	 * 
	 ***********************************************************/
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


		if(tileName == "Documents"){
			switch(direction){			
			case EVENT_NORTH:
				break;
			case EVENT_SOUTH:
				twineDisplay("found documents");
				soundHandler.playPapers();
				if(currentQuest == Q_FIND_PAPERS){
					currentQuest = Q_SEARCH_FOR_FAMILY_NAME;
				} else {
					// repeat look at papers, reminding of name
				}
				break;
			case EVENT_EAST:
				break;
			case EVENT_WEST:
				break;
			}
		}

		if(tileName == "Index"){
			switch(direction){			
			case EVENT_NORTH:
				break;
			case EVENT_SOUTH:
				break;
			case EVENT_EAST:
				break;
			case EVENT_WEST:
				if(currentQuest == Q_SEARCH_FOR_FAMILY_NAME){
					twineDisplay("library index");
				} else if(currentQuest == Q_SEARCH_LIBRARY_FOR_CLUE_AND_FIND_KEY){
					twineDisplay("library index 2");
				}
				else {
					// let user search for anything without getting any results
				}
				break;
			}
		}


		if(tileName == "Basement"){
			switch(direction){			
			case EVENT_NORTH:
				if(currentQuest == Q_NIGHTMARE_FIND_BASEMENT_DOOR){
					twineDisplay("basement door nightmare");
				} else if (currentQuest == Q_SEARCH_LIBRARY_FOR_CLUE_AND_FIND_KEY) {
					twineDisplay("basement door reality");
				} else {
					twineDisplay("basement door");
				}
				break;
			case EVENT_SOUTH:
				break;
			case EVENT_EAST:
				break;
			case EVENT_WEST:
				break;
			}
		}


		if(tileName == "Stairs up"){
			switch(direction){			
			case EVENT_NORTH:
				break;
			case EVENT_SOUTH:
				twineDisplay("stairs up");
				break;
			case EVENT_EAST:
				break;
			case EVENT_WEST:
				break;
			}
		}

		if(tileName == "Cafeteria"){
			switch(direction){			
			case EVENT_NORTH:
				break;
			case EVENT_SOUTH:
				twineDisplay("cafeteria door");
				break;
			case EVENT_EAST:
				break;
			case EVENT_WEST:
				break;
			}
		}

		if(tileName == "LoungeDoor"){
			switch(direction){			
			case EVENT_NORTH:
				break;
			case EVENT_SOUTH:
				break;
			case EVENT_EAST:
				break;
			case EVENT_WEST:
				twineDisplay("lounge door");
				break;
			}
		}


//		if(tileName == "Closet"){
//			switch(direction){			
//			case EVENT_NORTH:
//				break;
//			case EVENT_SOUTH:
//				if(currentQuest == Q_SEARCH_LIBRARY_FOR_CLUE_AND_FIND_KEY){
//					twineDisplay("closet");
//				}
//				break;
//			case EVENT_EAST:
//				break;
//			case EVENT_WEST:			
//				break;
//			}
//		}


		if(tileName == "EndPapers"){
			switch(direction){			
			case EVENT_NORTH:
				break;
			case EVENT_SOUTH:
				twineDisplay("end papers");
				soundHandler.playPapers();
				break;
			case EVENT_EAST:
				break;
			case EVENT_WEST:
				break;
			}
		}


		// Doors
		if(tileName == "ClassDoor1"){
			switch(direction){			
			case EVENT_EAST:
				player.currentTile.openDoor();
				player.currentTile.blockEast = false;
				break;
			}
		}
		if(tileName == "ClassDoor2"){
			switch(direction){
			case EVENT_WEST:
				player.currentTile.openDoor();
				player.currentTile.blockWest = false;
				break;
			case EVENT_EAST:
				twineDisplay("wc");
				break;
			}
		}
		if(tileName == "wc"){
			switch(direction){
			case EVENT_EAST:
				twineDisplay("wc");
				break;
			}
		}
		if(tileName == "ClassDoor3"){
			switch(direction){			
			case EVENT_EAST:
				player.currentTile.openDoor();
				player.currentTile.blockEast = false;
				break;
			}
		}
		if(tileName == "ClassDoor4"){
			switch(direction){			
			case EVENT_EAST:
				player.currentTile.openDoor();
				player.currentTile.blockEast = false;
				break;
			}
		}
		if(tileName == "ClassDoor5"){
			switch(direction){			
			case EVENT_EAST:
				player.currentTile.openDoor();
				player.currentTile.blockEast = false;
				break;
			}
		}
		if(tileName == "OfficeDoor"){
			switch(direction){			
			case EVENT_SOUTH:
				player.currentTile.openDoor();
				player.currentTile.blockSouth = false;
				twineDisplay("office");
				break;
			}
		}
		if(tileName == "LibraryDoor"){
			switch(direction){			
			case EVENT_WEST:
				player.currentTile.openDoor();
				player.currentTile.blockWest = false;
				twineDisplay("library");
				break;
			}
		}
		if(tileName == "LoungeDoor"){
			switch(direction){			
			case EVENT_WEST:
				//player.currentTile.openDoor();
				//player.currentTile.blockWest = false;
				break;
			}
		}
	}
	
	/***********************************************************
	 * 
	 * MOVEMENT
	 * 
	 ***********************************************************/
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
				if(currentQuest == -1){
					twineDisplay("enter school");
					currentQuest = Q_FIND_PAPERS;
				}
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
				if(currentQuest == Q_FIND_PAPERS){
				}
				//twineDisplay("office");
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
				//twineDisplay("library");
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
				//if(currentQuest == Q_NIGHTMARE_FIND_BASEMENT_DOOR){
				//	twineDisplay("basement door nightmare");
				//} else if (currentQuest == Q_ENTER_BASEMENT) {
				//	twineDisplay("unlock basement");
				//}
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











	private GameObject[] largeTables;

	public void showNightmareWorld(){
		currentQuest = Q_NIGHTMARE_FIND_BASEMENT_DOOR;
		audioSource.clip = nightmareAmbience;
		audioSource.Play();
		monoliths.SetActive(true);
		largeTables = GameObject.FindGameObjectsWithTag("largetable");
		foreach(GameObject table in largeTables){
			table.SetActive(false);
		}

		RenderSettings.fog = true;
	}

	public void hideNightmareWorld(){
		currentQuest = Q_SEARCH_LIBRARY_FOR_CLUE_AND_FIND_KEY;
		player.transform.position = new Vector3(-12.5f, 0f, -17.5f);
		audioSource.clip = normalAmbience;
		audioSource.Play();
		monoliths.SetActive(false);
		foreach(GameObject table in largeTables){
			table.SetActive(true);
		}
		RenderSettings.fog = false;
		knocking.SetActive(true);
	}


	public void goToBasement(){
		player.transform.position = new Vector3(2.5f, 0f, -10f);
		audioSource.Stop();
		knocking.SetActive(false);
	}


//	private void pickUpKey(){
//		currentQuest = Q_ENTER_BASEMENT;
//	}

	private void twineDisplay(string passageName){
		Application.ExternalCall("TwineDisplay", passageName);
	}

	public void stopMovement(){
		player.allowMovement = false;
	}
	
	public void startMovement(){
		player.allowMovement = true;
	}

	/////////////////////////
	/// debug stuff
	/////////////////////////

	void OnGUI() {
		//GUI.Label(new Rect(10, 10, 300, 300), "Current quest: " + currentQuest);		
	}
}
