using UnityEngine;
using System.Collections;

public class AddUpgradePanelsChecker : MonoBehaviour {

	

	public void CheckAddPanel() {
		GameObjectsManager.gameManager.GetComponent<TowerPlacer> ().PlaceTower ();
	}

	public void CheckUpgradePanel() {
		GameObjectsManager.gameManager.GetComponent<TowerPlacer> ().UpgradeTower ();
	}
}
