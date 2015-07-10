using UnityEngine;
using System.Collections.Generic;

public class SetResolutions : MonoBehaviour {

	public int[] resolutionHeights;

	private Resolution currentResolution;
	//private Dictionary<int, float> = new Dictionary<int, float>();
	private float ratio;
	private int h;
	private int intRes;

	void Start() {
		CheckResolution ();
	}
	
	public void ResolutionSlider(float resolution){
		intRes = (int)resolution;

		h = resolutionHeights [intRes];
	}

	public void CheckResolution() {
		currentResolution = Screen.currentResolution;
		float w = currentResolution.width;
		h = currentResolution.height;
		ratio = w / h;
	}

	public void SetResolution () {
		Screen.SetResolution ((int)(h * ratio), h, true);
	}
}
