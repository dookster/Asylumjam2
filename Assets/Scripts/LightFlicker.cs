using UnityEngine;
using System.Collections;

public class LightFlicker : MonoBehaviour {

	public Light lightObject;
	public float maxInterval;

	private float intensity;

	// Use this for initialization
	void Start () {
		intensity = lightObject.intensity;
		FlickLight();
	}
	
	// Update is called once per frame
	void Update () {

	}

	void FlickLight()
	{
		if(lightObject.intensity == 0)
		{
			lightObject.intensity = intensity;
		} else {
			lightObject.intensity = 0;
		}
		Invoke ("FlickLight", Random.Range(0, maxInterval));
	}

}
