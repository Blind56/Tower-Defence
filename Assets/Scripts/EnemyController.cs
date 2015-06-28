using UnityEngine;
using System.Collections;

public class EnemyController : MonoBehaviour {

	public float speed = 0.5f;
	[Range(0,100)]	
	public float health = 100;
	
	private TowerController towerController;
	private GameObject enemyPath;
	private Transform[] waypoints;
	private int i = 0;

	void Start () {
		speed /= 10;
		enemyPath = GameObject.FindGameObjectWithTag ("Enemy Path");
		waypoints = EnemySpawner.GetChildren (enemyPath);
	}

	void Update () {
		if (health <= 0) {
			//Debug.Log("I'm killed");
			Destroy (this.gameObject);
		}
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
		if (i == waypoints.Length) {
			Debug.Log("Game Over");
			Time.timeScale = 0;
			GameObject.FindGameObjectWithTag("Game manager").SetActive(false);
		}
		if (c.gameObject.tag == "Tower") {
			towerController = c.gameObject.GetComponent<TowerController>();
			towerController.target = this;
		}
	}
}
