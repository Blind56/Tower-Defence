﻿using UnityEngine;
using System.Collections;

public class TowerSpotsController : MonoBehaviour {

	// Use this for initialization
	void Start () {
		GameObjectsManager.towerSpots = this.gameObject;
	}
}
