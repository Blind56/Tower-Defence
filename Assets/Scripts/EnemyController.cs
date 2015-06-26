using UnityEngine;
using System.Collections.Generic;

public class EnemyController : MonoBehaviour {

	public float speed = 0.5f;

	private GameObject enemyPath;
	private Transform[] waypoints;
	private int i = 0;

	void Start () {
		speed /= 10;
		enemyPath = GameObject.FindGameObjectWithTag ("Enemy Path");
		waypoints = EnemySpawner.GetChildren (enemyPath);
	}


	void FixedUpdate () {
		if (i < waypoints.Length) {
			if (waypoints[i].position - transform.position != Vector3.zero) {
				transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.LookRotation((waypoints[i].position - transform.position).normalized), speed);
				transform.Translate(Vector3.forward * speed);
			}
		}
	}	

	void OnTriggerEnter (Collider c) {
		if (i < waypoints.Length) {
			if (c.gameObject.transform == waypoints [i]) {
				i++;
			}
		}
	}
}
