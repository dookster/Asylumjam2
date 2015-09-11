using UnityEngine;
using System.Collections;
using UnityEngine.UI;


public class Menu : MonoBehaviour {

	public Player player;

	[Header("Menu")]
	public GameObject MainPanel;
	public GameObject SettingsPanel;

	public Slider AlphaSlider;
	public Slider SizeSlider;

	[Header("Move buttons")]
	public CanvasGroup buttonGroup;
	public Button[] buttons;

	void Start()
	{
		AlphaSlider.value = GameSettings.LoadButtonAlpha();
		SizeSlider.value = GameSettings.LoadButtonSize();
		

		SetButtonAlpha(AlphaSlider.value);
		SetButtonSize(SizeSlider.value);
	}

	void Update()
	{
		if(Input.GetButtonUp("Cancel"))
		{
			if(MainPanel.activeSelf)
			{
				HideMenu();
			}
			else if(SettingsPanel.activeSelf)
			{
				HideSettings();
			}
			else 
			{
				ShowMenu();
			}
		}
	}

	public void ShowMenu()
	{
		MainPanel.SetActive(true);
		SettingsPanel.SetActive(false);
	}

	public void HideMenu()
	{
		MainPanel.SetActive(false);
		SettingsPanel.SetActive(false);
	}

	public void ShowCredits()
	{
		
	}

	public void HideCredits()
	{

	}

	public void ShowSettings()
	{
		SettingsPanel.SetActive(true);
		MainPanel.SetActive(false);
		if(player == null || !player.allowMovement)
		{
			ShowButtons();
		}
	}

	public void HideSettings()
	{
		SettingsPanel.SetActive(false);
		MainPanel.SetActive(true);
		if(player == null || !player.allowMovement)
		{
			HideButtons();
		}
	}

	public void SetButtonSize(float value)
	{
		foreach(Button b in buttons)
		{
			RectTransform rect = b.GetComponent<RectTransform>();
			rect.sizeDelta = new Vector2(value * 100, value * 100);
		}
		GameSettings.SaveButtonSize(value);
	}

	public void SetButtonAlpha(float value)
	{
		buttonGroup.alpha = value;
		GameSettings.SaveButtonAlpha(value);
	}

	public void Quit()
	{
		Application.Quit();
	}

	public void Restart()
	{
		// TODO Do any clean up?
		Application.LoadLevel(0);
	}

	public void Begin()
	{
		Application.LoadLevel(1);
	}

	public void HideButtons()
	{
		foreach(Button b in buttons)
		{
			if(b.name != "MenuButton") b.gameObject.SetActive(false);
		}
	}

	public void ShowButtons()
	{
		foreach(Button b in buttons)
		{
			if(b.name != "MenuButton") b.gameObject.SetActive(true);
		}
	}

}
