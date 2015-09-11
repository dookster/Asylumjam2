using UnityEngine;
using System.Collections;

public class GameSettings : MonoBehaviour {

	public const string UI_BUTTON_SIZE = "button_size";
	public const string UI_BUTTON_ALPHA = "button_alpha";
	public const string UI_BUTTON_LAYOUT = "buttons_layout";

	public static void SaveButtonSize(float size)
	{
		PlayerPrefs.SetFloat(UI_BUTTON_SIZE, size);
	}

	public static float LoadButtonSize()
	{
		return PlayerPrefs.GetFloat(UI_BUTTON_SIZE, 0.75f);
	}

	public static void SaveButtonAlpha(float alpha)
	{
		PlayerPrefs.SetFloat(UI_BUTTON_ALPHA, alpha);
	}

	public static float LoadButtonAlpha()
	{
		return PlayerPrefs.GetFloat(UI_BUTTON_ALPHA, 0.15f);
	}

	public static void SaveButtonLayout(int layout)
	{
		PlayerPrefs.SetInt(UI_BUTTON_LAYOUT, layout);
	}

	public static int LoadButtonLayout()
	{
		return PlayerPrefs.GetInt(UI_BUTTON_LAYOUT);
	}
}
