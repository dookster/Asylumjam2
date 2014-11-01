using UnityEngine;
using System.Collections;

public class SceneryRoll : MonoBehaviour {

	public float speed;

	private Vector3 startPosition;

	// Use this for initialization
	void Start () {
		startPosition = new Vector3(transform.position.x, transform.position.y, transform.position.z);
	}
	
	// Update is called once per frame
	void Update () {
		if(transform.position.z <= -35)
			transform.position = new Vector3(startPosition.x, startPosition.y, 70);

		transform.Translate(0, 0, -speed * Time.deltaTime);
	}
}
