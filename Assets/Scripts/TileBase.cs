using UnityEngine;
using System.Collections;


public class TileBase : MonoBehaviour {

	public bool blockNorth;
	public bool blockEast;
	public bool blockSouth;
	public bool blockWest;

	public bool fireOnce;



	// Use this for initialization
	void Start () {

	}
	
	// Update is called once per frame
	void Update () {
	
	}

	/**
	 * Draw gizmos showing blocked directions
	 */
	void OnDrawGizmos() {
		float drawHeight = 0;

		Gizmos.color = Color.cyan;
		Gizmos.DrawWireCube(transform.position, transform.localScale);

		float wallWidth = 0.1f;
		float halfSize = transform.localScale.x /2;
		float height = transform.localScale.y;

		Vector3 top = new Vector3(transform.position.x, drawHeight, transform.position.z + halfSize);
		Vector3 bottom = new Vector3(transform.position.x, drawHeight, transform.position.z - halfSize);
		Vector3 right = new Vector3(transform.position.x + halfSize, drawHeight, transform.position.z);
		Vector3 left = new Vector3(transform.position.x - halfSize, drawHeight, transform.position.z);

		Gizmos.color = Color.red;

		if(blockNorth)
			Gizmos.DrawCube(top, new Vector3(halfSize * 2, height, wallWidth));
		if(blockEast)
			Gizmos.DrawCube(right, new Vector3(wallWidth, height, halfSize * 2));
		if(blockSouth)
			Gizmos.DrawCube(bottom, new Vector3(halfSize * 2, height, wallWidth));
		if(blockWest)
			Gizmos.DrawCube(left, new Vector3(wallWidth, height, halfSize * 2));
	}

	public void onLookingNorth(){

	}

	public void onLookingSouth(){
		
	}

	public void onLookingEast(){
		
	}

	public void onLookingWest(){
		
	}

	public void onEnter(){

	}

	public void onExit(){

	}

	public void onUse(){

	}

}
