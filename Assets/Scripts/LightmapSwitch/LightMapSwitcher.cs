using UnityEngine;
using System.Linq;
using System.Collections.Generic;

public class LightMapSwitcher : MonoBehaviour
{
	public Texture2D[] Normal;
	public Texture2D[] Lightning;

	public List<Texture2D> normalDirection;
	public List<Texture2D> normalLight;
	
	public List<Texture2D> lightningDirection;
	public List<Texture2D> lightningLight;

	private LightmapData[] normalLightMaps;
	private LightmapData[] lightningLightMaps;
	
	void Start ()
	{

		normalDirection = new List<Texture2D>();
		normalLight = new List<Texture2D>();

		lightningDirection = new List<Texture2D>();
		lightningLight = new List<Texture2D>();

		// Sort into light and direction maps.
		foreach(Texture2D t2d in Normal)
		{
			if(t2d.name.Contains("dir"))
			{
				normalDirection.Add(t2d);
			}
			if(t2d.name.Contains("light"))
			{
				normalLight.Add(t2d);
			}
		}

		foreach(Texture2D t2d in Lightning)
		{
			if(t2d.name.Contains("dir"))
			{
				lightningDirection.Add(t2d);
			}
			if(t2d.name.Contains("light"))
			{
				lightningLight.Add(t2d);
			}
		}

		// Sort the Day and Night arrays in numerical order, so you can just blindly drag and drop them into the inspector
		normalDirection = (List<Texture2D>) normalDirection.OrderBy(t2d => t2d.name, new NaturalSortComparer<string>()).ToList();
		normalLight = (List<Texture2D>) normalLight.OrderBy(t2d => t2d.name, new NaturalSortComparer<string>()).ToList();
		lightningDirection = (List<Texture2D>) lightningDirection.OrderBy(t2d => t2d.name, new NaturalSortComparer<string>()).ToList();
		lightningLight = (List<Texture2D>) lightningLight.OrderBy(t2d => t2d.name, new NaturalSortComparer<string>()).ToList();


		// Put them in a LightMapData structure

		//
		// NEAR = DIRECTIONAL
		// FAR = LIGHT
		//

		normalLightMaps = new LightmapData[normalDirection.Count];
		for (int i = 0 ; i < normalDirection.Count; i++)
		{
			normalLightMaps[i] = new LightmapData();
			normalLightMaps[i].lightmapNear = normalDirection[i];
			normalLightMaps[i].lightmapFar = normalLight[i];
		}
		
		lightningLightMaps = new LightmapData[lightningDirection.Count];
		for (int i = 0 ; i < lightningDirection.Count ; i++)
		{
			lightningLightMaps[i] = new LightmapData();
			lightningLightMaps[i].lightmapNear = lightningDirection[i];
			lightningLightMaps[i].lightmapFar = lightningLight[i];
		}
	}
	
	#region Publics
	public void SetToDay()
	{
		LightmapSettings.lightmaps = normalLightMaps;
		
	}
	
	public void SetToNight()
	{
		LightmapSettings.lightmaps = lightningLightMaps;
	}
	#endregion
	
	#region Debug
	[ContextMenu ("Set to Night")]
	void Debug00()
	{
		SetToNight();
	}
	
	[ContextMenu ("Set to Day")]
	void Debug01()
	{
		SetToDay();
	}
	#endregion
}