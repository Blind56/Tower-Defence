using UnityEngine;
using System.Collections;

public class CameraMovement : MonoBehaviour {
	
	[Range(0, 1f)]
	public float cameraSensitivity = 0.2f;

	private bool inputType;
	private float x, z;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void FixedUpdate () {
		if (Input.touchSupported) {
			if (Input.touchCount > 0) {
				Touch touch = Input.touches[0];
				inputType = touch.phase == TouchPhase.Moved;
				x = touch.deltaPosition.x * (cameraSensitivity / (Screen.currentResolution.height / 100));
				z =  touch.deltaPosition.y * (cameraSensitivity / (Screen.currentResolution.height / 100));
			}

		}
		else {
			inputType = true;
			x = -Input.GetAxis ("Horizontal");
			z = -Input.GetAxis ("Vertical");
		}
		if (inputType) {
			transform.position += (new Vector3 (x * cameraSensitivity, 0, z * cameraSensitivity));
		}
	}
}
