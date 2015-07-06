using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Timer : MonoBehaviour {

	public float countFrom = 10;

	private ShowPanels showPanels;
	private GameObject timer;
	private Text timerText;

	// Use this for initialization
	void Start () {
		showPanels = GameObjectsManager.UI.GetComponent<ShowPanels> ();
		showPanels.ShowTimer ();
		timer = showPanels.timer;
		timerText = timer.GetComponent<Text> ();
	}
	
	// Update is called once per frame
	void Update () {
		if (countFrom >= 0) {
			countFrom -= Time.deltaTime;
			timerText.text = string.Format("{0:0.0}", countFrom);
			//Debug.Log (countFrom);
		} else {
			GameObjectsManager.gameManager.GetComponent<GameManager>().BeginWave();
			showPanels.HideTimer();
		}
	}
}
