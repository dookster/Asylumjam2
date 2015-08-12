using UnityEngine;
using System.Collections;

/**
 * Editor script for keeping the object snapped to a grid on x/z with the given spacing
 * 
 */
[ExecuteInEditMode]
public class EditSnapToGrid : MonoBehaviour {

	public float snapValue = 2;
	public float depth = 0;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		float snapInverse = 1/snapValue;
		float x, y, z;

		x = Mathf.Round(transform.position.x * snapInverse)/snapInverse;
		y = depth;  // height
		z = Mathf.Round(transform.position.z * snapInverse)/snapInverse; 

		transform.position = new Vector3(Mathf.Round(x*100.0f)/100.0f, y, Mathf.Round(z * 100.0f) / 100.0f);
	}



}
