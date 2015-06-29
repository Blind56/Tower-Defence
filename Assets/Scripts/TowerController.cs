using UnityEngine;
using System.Collections;

public class TowerController : MonoBehaviour {
	
	public float fireRate = 1;
	public EnemyController target;
	public GameObject projector;
	public int currLevel = 1;
	public float[] upgradeCosts;
	public float[] damages;
	public float[] radiuses;

	private TowerPlacer gameManager;
	private bool canRecieveDamage = true;

	void Start () {
		currLevel = 1;
		currLevel = Mathf.Clamp (currLevel, 1, damages.Length);
	}

	void Update () {
		if (target != null) {
			Debug.DrawLine(transform.position, target.transform.position);
			if (canRecieveDamage) {
				StartCoroutine(TakeDamage(fireRate));
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
