using UnityEngine;
using System.Collections;

public class GameManager : MonoBehaviour {

	public float money;

	private Transform[] text;
	

	public void LowerResolution(){
		Screen.SetResolution (854, 480, true);
	}	
}
