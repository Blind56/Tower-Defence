using UnityEngine;
using System.Collections;

public class CameraMovement : MonoBehaviour {
	
	[Range(0, 1f)]
	public float cameraSensitivity = 0.2f;
	public float orthoZoomSpeed = .5f;
	public float perspectiveZoomSpeed = .5f;
	public float maxOrthoSize = 20;
	public float minOrthoSize = 5;
	public float maxFOV = 60;
	public float minFOV = 20;

	private bool inputType;
	private float x, z;

	void Update () {
		if (Input.touchCount == 2) {
			Touch touchZero = Input.GetTouch (0);
			Touch touchOne = Input.GetTouch (1);
			Vector2 touchZeroPrevPos = touchZero.position - touchZero.deltaPosition;
			Vector2 touchOnePrevPos = touchOne.position - touchOne.deltaPosition;

			float prevTouchDeltaMag = (touchZeroPrevPos - touchOnePrevPos).magnitude;
			float touchDeltaMag = (touchZero.position - touchOne.position).magnitude;

			float deltaMagnitudeDiff = prevTouchDeltaMag - touchDeltaMag;

			if (Camera.main.orthographic) {
				Camera.main.orthographicSize += deltaMagnitudeDiff * orthoZoomSpeed;
				Camera.main.orthographicSize = Mathf.Clamp (Camera.main.orthographicSize, minOrthoSize, maxOrthoSize);
			} else {
				Camera.main.fieldOfView += deltaMagnitudeDiff * perspectiveZoomSpeed;
				Camera.main.fieldOfView = Mathf.Clamp (Camera.main.fieldOfView, minFOV, maxFOV);
			}
		}
	}

	void FixedUpdate () 
	{
		if (Input.touchSupported) {
			if (Input.touchCount == 1) {
				Touch touch = Input.touches[0];
				inputType = touch.phase == TouchPhase.Moved;
				x = touch.deltaPosition.x * (cameraSensitivity / (Screen.currentResolution.height / 100));
				z =  touch.deltaPosition.y * (cameraSensitivity / (Screen.currentResolution.height / 100));
			}

		}
		else {
			inputType = true;
			x = Input.GetAxis ("Horizontal");
			z = Input.GetAxis ("Vertical");
		}
		if (inputType) {
			transform.Translate (new Vector3(0,1,1) * z * cameraSensitivity);
			transform.Translate (Vector3.right * x * cameraSensitivity);
		}
	}
}
