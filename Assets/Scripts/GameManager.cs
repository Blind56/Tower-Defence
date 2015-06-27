using UnityEngine;
using System.Collections;

public class GameManager : MonoBehaviour {

	public float money;
	public bool begin;
	
	private Transform[] text;

	public void LowerResolution(){
		Screen.SetResolution (854, 480, true);
	}	

	public void Restart() {
		Application.LoadLevel (0);
	}

	public void BeginWave() {
		if (!begin) {
			begin = true;
		} else {
			begin = false;
		}
	}
}
