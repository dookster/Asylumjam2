using UnityEngine;
using System.Collections;

[ExecuteInEditMode]
public class FlexWall : MonoBehaviour {

	public Transform oneEdge;
	public Transform otherEdge;

	public float snapValue = 2;
	public float snapOffset;
	public float depth = 0;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {

		// Fix rotation to cardinals


		//Figure out the rotation


		// Fix position of edge transforms
		float snapInverse = 1/snapValue;
		float x, y, z;
		
		x = Mathf.Round(oneEdge.transform.position.x * snapInverse)/snapInverse;
		y = depth;  // height
		z = Mathf.Round(oneEdge.transform.position.z * snapInverse)/snapInverse; 
		
		oneEdge.transform.position = new Vector3(Mathf.Round(x*100.0f)/100.0f, y, Mathf.Round(z * 100.0f) / 100.0f);


		x = Mathf.Round(otherEdge.transform.position.x * snapInverse)/snapInverse;
		y = depth;  // height
		z = Mathf.Round(otherEdge.transform.position.z * snapInverse)/snapInverse; 
		
		otherEdge.transform.position = new Vector3(Mathf.Round(x*100.0f)/100.0f, y, Mathf.Round(z * 100.0f) / 100.0f);

	}
}
