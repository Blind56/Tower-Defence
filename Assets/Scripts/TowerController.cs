using UnityEngine;
using System.Collections.Generic;

public class TowerController : MonoBehaviour {

	public float cost;
	public float fireRate = 1;
	public int currLevel = 1;
	public float[] damages;

	void Start () {
		if (currLevel < 1) {
			currLevel = 1;
		}
		if (currLevel > damages.Length) {
			currLevel = damages.Length;
		}
	}
}
