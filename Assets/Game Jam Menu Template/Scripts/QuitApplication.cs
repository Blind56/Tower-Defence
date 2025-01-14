﻿using UnityEngine;
using System.Collections;

public class QuitApplication : MonoBehaviour {

	public void Quit()
	{
		//If we are running in a standalone build of the game
	#if UNITY_ANDROID
		//Quit the application
		Application.Quit();
	#endif

		//If we are running in the editor
	#if UNITY_EDITOR
		//Stop playing the scene
		UnityEditor.EditorApplication.isPlaying = false;
	#endif
	}

	public void Back () {
		Time.timeScale = 1;
		Application.LoadLevel (0);
		GameObject.Destroy (this.gameObject);
	}
}
