using UnityEngine;
using System.Collections;

public class GameManager : MonoBehaviour {

	public float money;
	
	private Transform[] text;

	void Start () {
		GameObjectsManager.gameManager = this.gameObject;
		Time.timeScale = 1;
	}

	public void LowerResolution(){
		Screen.SetResolution (960, 540, true);
	}	

	public void Restart() {
		Application.LoadLevel (0);
	}
}
