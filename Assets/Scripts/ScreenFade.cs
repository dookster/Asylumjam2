using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class ScreenFade : MonoBehaviour {

	public Image ScreenBlock;
	public float FadeTime;

	// Use this for initialization
	void Start () {
		FadeToClear();
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void FadeToBlack()
	{
		ScreenBlock.color = Color.black;
		ScreenBlock.CrossFadeAlpha(1f, FadeTime, false);
	}

	public void FadeToClear()
	{
		ScreenBlock.CrossFadeAlpha(0f, FadeTime, false);
	}

	public void FadeToWhite()
	{
		ScreenBlock.color = Color.white;
		ScreenBlock.CrossFadeAlpha(1f, FadeTime, false);
	}
}
