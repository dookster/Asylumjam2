using UnityEngine;
using System.Collections;

public class NightSky : MonoBehaviour {

	public ParticleSystem particleSystem;

	public Transform player;

	// Use this for initialization
	void Start () {
		particleSystem.Pause();
	}
	
	// Update is called once per frame
	void Update () {
		if(player != null)
		{
			transform.position = player.position;
		}
	}
}
