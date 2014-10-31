using UnityEngine;
using System.Collections;

/**
 * Base class for a tile, a 2x2 area wich can be blocked (exit) in any of
 * the four directions.
 * 
 * Can fire an event on:
 *  - enter
 *  - exit
 *  - look in any direction
 * 
 * All events can be set to only fire once.
 * 
 * Tiles with nothing but blocked exits should just use this script, tiles needing more specific events
 * should have a script extending this one.
 */
public class TileBase : MonoBehaviour {

	public bool blockNorth;
	public bool blockEast;
	public bool blockSouth;
	public bool blockWest;


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

		float wallWidth = 0.2f;
		float halfSize = transform.localScale.x /2;

		Vector3 top = new Vector3(transform.position.x, drawHeight, transform.position.z + halfSize);
		Vector3 bottom = new Vector3(transform.position.x, drawHeight, transform.position.z - halfSize);
		Vector3 right = new Vector3(transform.position.x + halfSize, drawHeight, transform.position.z);
		Vector3 left = new Vector3(transform.position.x - halfSize, drawHeight, transform.position.z);

		Gizmos.color = Color.red;

		if(blockNorth)
			Gizmos.DrawCube(top, new Vector3(halfSize * 2, halfSize * 2, wallWidth));
		if(blockEast)
			Gizmos.DrawCube(right, new Vector3(wallWidth, halfSize * 2, halfSize * 2));
		if(blockSouth)
			Gizmos.DrawCube(bottom, new Vector3(halfSize * 2, halfSize * 2, wallWidth));
		if(blockWest)
			Gizmos.DrawCube(left, new Vector3(wallWidth, halfSize * 2, halfSize * 2));
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

}
