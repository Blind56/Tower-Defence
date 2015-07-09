using UnityEngine;
using System.Collections.Generic;
using System.Collections;

public class EnemySpawner : MonoBehaviour {

	public GameObject enemy;
	public int numberOfEnemies = 1;
	public float groundClearence = 0.5f;
	public float spawnDelay = 1f;
	[HideInInspector]
	public bool begin = false;
	
	private GameObject enemyPath;
	private Transform[] waypoints;
	private bool spawned;
	private int actualNumberOfEnemies;

	// Use this for initialization
	void Start () {
		enemyPath = GameObjectsManager.enemyPath;
		waypoints = EnemySpawner.GetChildren (enemyPath);
	}

	void Update () {
		if (!spawned) {
			if (begin) {
				if (actualNumberOfEnemies < numberOfEnemies) {
					StartCoroutine (SpawnEnemy (spawnDelay));
				}
				else {
					begin = false;
					actualNumberOfEnemies = 0;
				}
			}
		}
	}
	
	// Update is called once per frame
	IEnumerator SpawnEnemy (float seconds){
		waypoints [0].LookAt (waypoints [1].position);
		Instantiate (enemy, new Vector3(waypoints [0].position.x, waypoints [0].position.y + groundClearence, waypoints [0].position.z), waypoints[0].rotation);
		actualNumberOfEnemies++;
		spawned = true;
		yield return new WaitForSeconds (seconds);
		spawned = false;
	}

	public static Transform[] GetChildren(GameObject parent)
	{
		Transform a = parent.transform;
		List<Transform> children = new List<Transform>();
		foreach (Transform b in a)
		{
			children.Add(b);
		}
		return children.ToArray();
	}
}
