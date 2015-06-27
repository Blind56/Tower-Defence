using UnityEngine;
using System.Collections.Generic;

public class TowerController : MonoBehaviour {
	
	public float fireRate = 1;
	public int currLevel = 1;
	public float[] upgradeCosts;
	public float[] damages;

	void Start () {
		currLevel = 1;
		currLevel = Mathf.Clamp (currLevel, 1, damages.Length);
	}
}
