using UnityEngine;
using System.Collections;

public class Lightning : MonoBehaviour {

	public LightMapSwitcher LightSwitcher;

	public Light lightningLight;
	public Light localLight;

	private float originalLightIntensity;

	// Use this for initialization
	void Start () {
		//originalLightIntensity = lightningLight.intensity;
		StartCoroutine(LightningFlash());
	}
	
	// Update is called once per frame
	void Update () {
//		if(lightningLight.intensity > 0)
//		{
//			lightningLight.intensity -= 20f * Time.deltaTime;
//		}
//		if(lightningLight.intensity < 0)
//		{
//			lightningLight.intensity = 0;
//		}
		//localLight.intensity = lightningLight.intensity;
	}

	IEnumerator LightningFlash()
	{
		// Lightning on
		//RenderSettings.ambientIntensity = 0.6f;
		//lightningLight.intensity = originalLightIntensity;
		LightSwitcher.SetToNight();

		// wait
		yield return new WaitForSeconds(Random.Range(0.1f, 0.25f));
		//lightningLight.intensity = originalLightIntensity/2;
		LightSwitcher.SetToDay();
		// wait random and flash again 
		yield return new WaitForSeconds(Random.Range(2, 5));
		StartCoroutine(LightningFlash());
	}



}
