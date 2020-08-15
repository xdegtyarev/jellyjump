using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class GraphicsConfig : MonoBehaviour
{
	[SerializeField] Material jelly;
	[SerializeField] Camera cam;
	[SerializeField] Light light;
	[SerializeField] float lowSat = 0.2f;
	[SerializeField] float highSat = 0.8f;
	[SerializeField] float lowVal = 0.2f;
	[SerializeField] float highVal = 0.2f;
	void Awake(){
		RenderSettings.fog = true;
		RenderSettings.fogMode = FogMode.Linear;
		RenderSettings.fogStartDistance = 1f;
		RenderSettings.fogEndDistance = 40f;
	}

	void OnEnable(){
		RandomizeColors();
	}

	void RandomizeColors(){
		float hue = Random.value;
		float rHue = 1f-hue;
		bool darkMode = Random.value > 0.5f;
		Color bgColor = Color.HSVToRGB(rHue, darkMode ? highSat : lowSat, darkMode ? highVal : lowVal);
		RenderSettings.fogColor = bgColor;
		cam.backgroundColor = bgColor;
		Color frColor = Color.HSVToRGB(hue, darkMode ? lowSat: highSat, highVal);
		light.color = frColor;
		Color jellyColor = Color.HSVToRGB(rHue,1f,1f);
		jelly.SetColor("_EmissionColor", jellyColor);
	}
}