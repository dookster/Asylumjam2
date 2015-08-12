using UnityEngine;
using System.Collections;

public class Player : MonoBehaviour {
	 
	public float movementSpeed = 0.2f;
	public float animateSpeed = 0.2f;
	public float moveLength = 2.5f;

	public Camera mainCamera;

	public GameObject forwardArrow;
	public GameObject leftArrow;
	public GameObject rightArrow;

	public GameObject uiArrows;

	public bool allowMovement;

	private const int FORWARD = 0;
	private const int LEFT = 1;
	private const int RIGHT = 2;

	private bool usePressed;

	private int nextMove = -1;

	private StoryHandler storyHandler;

	public TileBase currentTile;

	// Use this for initialization
	void Start () {
		storyHandler = GameObject.FindGameObjectWithTag("storyhandler").GetComponent<StoryHandler>() as StoryHandler;
		if(storyHandler == null) Debug.Log("ERROR: NO STORY HANDLER");
	}
	
	// Update is called once per frame
	void Update () {
//		if(allowMovement)
//			uiArrows.SetActive(true);
//		else
//			uiArrows.SetActive(false);


		if(allowMovement) {
			Ray clickRay = mainCamera.ScreenPointToRay(Input.mousePosition);
			RaycastHit hit;
			if(Physics.Raycast(clickRay, out hit, Mathf.Infinity) && Input.GetMouseButtonDown(0)){
				if(hit.collider == forwardArrow.GetComponent<Collider>()){
					nextMove = FORWARD;
				}
				if(hit.collider == leftArrow.GetComponent<Collider>()){
					nextMove = LEFT;
				}
				if(hit.collider == rightArrow.GetComponent<Collider>()){
					nextMove = RIGHT;
				}
			}

			if(Input.GetButtonDown("Forward")){
				nextMove = FORWARD;
			}
			if(Input.GetButtonDown("Left")){
				nextMove = LEFT;
			}
			if(Input.GetButtonDown("Right")){
				nextMove = RIGHT;
			}
			if(Input.GetButtonUp("Use")){
				onUse();
			}
		}


		if(iTween.Count(gameObject) == 0){
			if(allowMovement){
				if(Input.GetButton("Forward")){
					nextMove = FORWARD;
				}
				if(Input.GetButton("Left")){
					nextMove = LEFT;
				}
				if(Input.GetButton("Right")){
					nextMove = RIGHT;
				}

				switch(nextMove){
				case FORWARD:
					// Check if forward is blocked here
					bool blocked = false;
					if(Mathf.RoundToInt(transform.forward.z) > 0 && currentTile.blockNorth){
						// north
						blocked = true;
					}

					if(Mathf.RoundToInt(transform.forward.z) < 0 && currentTile.blockSouth){
						// south
						blocked = true;
					}

					if(Mathf.RoundToInt(transform.forward.x) < 0 && currentTile.blockWest){
						// west
						blocked = true;
					}

					if(Mathf.RoundToInt(transform.forward.x) > 0 && currentTile.blockEast){
						// east
						blocked = true;
					}

					if(!blocked)
						iTween.MoveBy(gameObject,iTween.Hash("z", moveLength, "time",movementSpeed, "oncomplete", "afterMoving", "oncompletetarget", gameObject));			
					nextMove = -1;
					break;
				case LEFT:
					iTween.RotateBy(gameObject,iTween.Hash("y",-(1f/4f),"time",movementSpeed, "oncomplete", "afterMoving", "oncompletetarget", gameObject));
					nextMove = -1;
					break;
				case RIGHT:
					iTween.RotateBy(gameObject,iTween.Hash("y",(1f/4f),"time",movementSpeed, "oncomplete", "afterMoving", "oncompletetarget", gameObject));
					nextMove = -1;
					break;
				}

			} else {
				nextMove = -1;
			}

		}
	}

	void OnTriggerEnter(Collider other) {
		TileBase enteredTile = (TileBase) other.gameObject.GetComponent<TileBase>();
		if(enteredTile != currentTile){
			currentTile = enteredTile;
			if(currentTile != null){
				storyHandler.handleMoveEvent(currentTile.name, StoryHandler.EVENT_ENTER);
			}
		}

	}

	void OnTriggerStay(Collider other){

	}

	private void onUse(){
		if(currentTile != null){
			if(Mathf.RoundToInt(transform.forward.z) > 0){
				// north
				storyHandler.handleUseEvent(currentTile.name, StoryHandler.EVENT_NORTH);
			}
			
			if(Mathf.RoundToInt(transform.forward.z) < 0){
				// south
				storyHandler.handleUseEvent(currentTile.name, StoryHandler.EVENT_SOUTH);
			}
			
			if(Mathf.RoundToInt(transform.forward.x) < 0){
				// west
				storyHandler.handleUseEvent(currentTile.name, StoryHandler.EVENT_WEST);
			}
			
			if(Mathf.RoundToInt(transform.forward.x) > 0){
				// east
				storyHandler.handleUseEvent(currentTile.name, StoryHandler.EVENT_EAST);
			}
		}
	}

	private void afterMoving(){
		// even out rotation
		straightenUp();

		// fire any events for the current tile to the story handler
		if(currentTile != null){
			if(Mathf.RoundToInt(transform.forward.z) > 0){
				// north
				storyHandler.handleMoveEvent(currentTile.name, StoryHandler.EVENT_NORTH);
			}
			
			if(Mathf.RoundToInt(transform.forward.z) < 0){
				// south
				storyHandler.handleMoveEvent(currentTile.name, StoryHandler.EVENT_SOUTH);
			}
			
			if(Mathf.RoundToInt(transform.forward.x) < 0){
				// west
				storyHandler.handleMoveEvent(currentTile.name, StoryHandler.EVENT_WEST);
			}
			
			if(Mathf.RoundToInt(transform.forward.x) > 0){
				// east
				storyHandler.handleMoveEvent(currentTile.name, StoryHandler.EVENT_EAST);
			}
		}
	}

	/**
	 * Round rotation after turning, avoid little rounding errors building up over time
	 */
	private void straightenUp(){
		transform.localRotation = new Quaternion(Mathf.Round(transform.localRotation.x), 
		                                         Mathf.Round(transform.localRotation.y), 
		                                         Mathf.Round(transform.localRotation.z), 
		                                         Mathf.Round(transform.localRotation.w));

		// TODO find a way to round position to half?
		//transform.position = new Vector3(Mathf.Round(transform.position.x), transform.position.y, Mathf.Round(transform.position.z));
	}

	public void turnLeft(){
		if(allowMovement)
			nextMove = LEFT;
	}
	public void turnRight(){
		if(allowMovement)
			nextMove = RIGHT;
	}
	public void goForward(){
		if(allowMovement)
			nextMove = FORWARD;
	}

	void OnDrawGizmos() {

		Gizmos.color = Color.green;
		Gizmos.DrawCube(transform.position, new Vector3(0.4f, 0.4f, 0.4f));

		//Gizmos.color = Color.magenta;
		Gizmos.DrawCube(transform.position + (transform.forward/3), new Vector3(0.3f, 0.3f, 0.3f));

	}
	

}
