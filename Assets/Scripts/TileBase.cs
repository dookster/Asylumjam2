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
}
