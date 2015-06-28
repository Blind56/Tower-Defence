using UnityEngine;
using System.Collections;

public class TowerController : MonoBehaviour {
	
	public float fireRate = 1;
	//public GameObject target;
	public EnemyController target;
	public int currLevel = 1;
	public float[] upgradeCosts;
	public float[] damages;
	public float[] radiuses;

	private bool canRecieveDamage = true;

	void Start () {
		currLevel = 1;
		currLevel = Mathf.Clamp (currLevel, 1, damages.Length);
	}

	void Update () {
		if (target != null) {
			Debug.DrawLine(transform.position, target.transform.position);
			//targetController = target.GetComponent<EnemyController>();
			if (canRecieveDamage) {
				StartCoroutine(TakeDamage(fireRate));
				//Debug.Log("I'm about to get hit!");
			}
		}
	}

	IEnumerator TakeDamage (float frequency) {
		canRecieveDamage = false;
		target.health -= damages [currLevel - 1];
		target.health = Mathf.Max (target.health, 0);
		yield return new WaitForSeconds(frequency);
		canRecieveDamage = true;
	}
}
