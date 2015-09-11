using UnityEngine;
using System.Collections;

public class LightFlicker : MonoBehaviour {

	public Light light;
	public float maxInterval;

	private float intensity;

	// Use this for initialization
	void Start () {
		intensity = light.intensity;
		FlickLight();
	}
	
	// Update is called once per frame
	void Update () {

	}

	void FlickLight()
	{
		if(light.intensity == 0)
		{
			light.intensity = intensity;
		} else {
			light.intensity = 0;
		}
		Invoke ("FlickLight", Random.Range(0, maxInterval));
	}

}
