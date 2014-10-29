using UnityEngine;
using System.Collections;

public class Player : MonoBehaviour {
	 
	public float movementSpeed = 0.2f;
	public float animateSpeed = 0.2f;

	public Camera mainCamera;

	public GameObject forwardArrow;
	public GameObject leftArrow;
	public GameObject rightArrow;
	
	public bool allowMovement;

	private const int FORWARD = 0;
	private const int LEFT = 1;
	private const int RIGHT = 2;

	private int nextMove = -1;

	// Use this for initialization
	void Start () {

	}
	
	// Update is called once per frame
	void Update () {

		if(allowMovement) {
			Ray clickRay = mainCamera.ScreenPointToRay(Input.mousePosition);
			RaycastHit hit;
			if(Physics.Raycast(clickRay, out hit, Mathf.Infinity) && Input.GetMouseButtonDown(0)){
				if(hit.collider == forwardArrow.collider){
					nextMove = FORWARD;
				}
				if(hit.collider == leftArrow.collider){
					nextMove = LEFT;
				}
				if(hit.collider == rightArrow.collider){
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
					iTween.MoveBy(gameObject,iTween.Hash("z",2,"time",movementSpeed, "oncomplete", "straightenUp", "oncompletetarget", gameObject));			
					nextMove = -1;
					break;
				case LEFT:
					iTween.RotateBy(gameObject,iTween.Hash("y",-(1f/4f),"time",movementSpeed, "oncomplete", "straightenUp", "oncompletetarget", gameObject));
					nextMove = -1;
					break;
				case RIGHT:
					iTween.RotateBy(gameObject,iTween.Hash("y",(1f/4f),"time",movementSpeed, "oncomplete", "straightenUp", "oncompletetarget", gameObject));
					nextMove = -1;
					break;
				}

			} else {
				nextMove = -1;
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
		transform.position = new Vector3(Mathf.Round(transform.position.x), transform.position.y, Mathf.Round(transform.position.z));
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
	

}
