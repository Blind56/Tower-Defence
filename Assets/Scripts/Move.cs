using UnityEngine;
using System.Collections;

public class Move : MonoBehaviour {

	public float speed = 0.5f;
	public float maxSpeed = 5f;
	public float cooldownTime = 1f;
	public float respawnTime = 1f;

	private bool operable = true;
	private bool inputType = true;
	private Rigidbody rb;
	private float x, z;

	void Start() {
		rb = this.GetComponent <Rigidbody> ();
	}

	void FixedUpdate(){
		//Debug.Log (rb.velocity.magnitude + "");
		if (operable) {
			if (rb.velocity.magnitude < maxSpeed) {
				if (Input.touchSupported) {
					Touch touch = Input.touches[0];
					inputType = touch.phase == TouchPhase.Moved;
					x = -touch.deltaPosition.x / 2;
					z =  -touch.deltaPosition.y / 2;
				}
				else {
					inputType = true;
					x = -Input.GetAxis ("Horizontal");
					z = -Input.GetAxis ("Vertical");
				}
				if (inputType) {
					rb.AddForce (new Vector3 (x * speed, 0, z * speed));
					rb.AddForce (transform.forward * speed * x);
					rb.AddForce (transform.right * speed * z);
				}
			}
		}
	}

	void OnTriggerExit() {
		StartCoroutine (Respawn(respawnTime));
	}

	IEnumerator Respawn (float seconds) {
		operable = false;
		yield return new WaitForSeconds (seconds);
		transform.position = new Vector3 (0, 1, 0);
		transform.rotation = new Quaternion (0, 0, 0, 0);
		rb.velocity = Vector3.zero;
		rb.angularVelocity = Vector3.zero;
		StartCoroutine (CoolDown(cooldownTime));
	}

	IEnumerator CoolDown (float seconds) {
		yield return new WaitForSeconds (seconds);
		operable = true;
	}
}
