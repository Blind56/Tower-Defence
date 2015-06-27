using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;

public class TowerPlacer : MonoBehaviour {

	public GameObject tower;
	public float groundClearence = 0.5f;
	
	private GameManager gameManager;
	private TowerController towerController;
	private GameObject addPanel;
	private GameObject upgradePanel;
	private GameObject towerSpots;
	private GameObject currentSpot;
	private Transform[] spots;
	private Transform spot;
	private bool inputType;
	private bool poppedUp;
	private bool spotFound;
	private bool placed;
	private bool misclicked;
	private Vector3 position;
	private Vector3 placePosition;
	private Vector3 cameraCurrPosition;
	private Touch touch;
	private int fingerId;

	void Start() { 
		addPanel = GameObject.FindGameObjectWithTag("Add panel");
		upgradePanel = GameObject.FindGameObjectWithTag("Upgrade panel");
		towerSpots = GameObject.FindGameObjectWithTag ("Tower spots");
		towerController = tower.GetComponent<TowerController>();
		gameManager = GetComponent<GameManager>();
		spots = EnemySpawner.GetChildren (towerSpots);
	}

	void Update () {
		if (Input.touchSupported) {
			if (Input.touchCount > 0) {
				touch = Input.touches [0];
				inputType = touch.phase == TouchPhase.Began;
				position = touch.position;
				fingerId =  Input.touches[0].fingerId;
			}
		} else {
			inputType = Input.GetButtonDown ("Fire1");
			position = Input.mousePosition;
		}
		if (inputType) {
			Ray ray = Camera.main.ScreenPointToRay (position);
			RaycastHit hit = new RaycastHit ();

			spotFound = false;
			misclicked = true;
			if (Physics.Raycast (ray, out hit)) {

				foreach (Transform s in spots) {
					if (hit.collider.gameObject == s.gameObject) {
						spot = s;
						spotFound = true;
					}
				}
				if (Input.touchSupported) {
					if (EventSystem.current.IsPointerOverGameObject (fingerId)) {
						spotFound = true;
					}				
				}
				else {
					if (EventSystem.current.IsPointerOverGameObject ()) {
						spotFound = true;
					}
				}
				if (!spotFound) {
					currentSpot = null;
				}
				if (!poppedUp) {
					if (spotFound) {
						currentSpot = spot.gameObject;
						placed = currentSpot.gameObject.GetComponent<SpotState> ().placed;
						if (placed) {
							upgradePanel.GetComponent<RectTransform> ().position = position;
						}
						else {
							addPanel.GetComponent<RectTransform> ().position = position;
						}
						placePosition = spot.transform.position;
						//Debug.DrawLine (ray.origin, hit.point);
						poppedUp = true;
						misclicked = false;						
					} else {
						currentSpot = null;
					}
				} else {
					if (misclicked) { /*&& urrentSpot != spot.gameObject)*/
						if (currentSpot != spot.gameObject) {
							addPanel.GetComponent<RectTransform> ().position = new Vector2 (Screen.currentResolution.width + 200, Screen.currentResolution.height + 200);
							upgradePanel.GetComponent<RectTransform> ().position = new Vector2 (Screen.currentResolution.width + 200, Screen.currentResolution.height + 200);
							currentSpot = null;
							poppedUp = false;
							misclicked = false;
						}
					}
				}
			}
		}
		if (poppedUp) {
			if (Camera.main.transform.position != cameraCurrPosition) {
				addPanel.GetComponent<RectTransform> ().position = Camera.main.WorldToScreenPoint(spot.transform.position);
			}
			cameraCurrPosition = Camera.main.transform.position;
		}
	}

	public void PlaceTower() {
		if (!placed) {
			if (gameManager.money >= towerController.upgradeCosts[towerController.currLevel - 1]) { 	
				gameManager.money -= towerController.upgradeCosts[towerController.currLevel - 1];
				Instantiate (tower, new Vector3 (placePosition.x, placePosition.y + groundClearence, placePosition	.z), Quaternion.Euler (0, 45, 0));
				GameObject[] towersArr = GameObject.FindGameObjectsWithTag("Tower");
				SpotState spotState = currentSpot.GetComponent<SpotState>();
				spotState.placed = true;	
				spotState.towerPlaced = towersArr[towersArr.Length - 1];
				addPanel.GetComponent<RectTransform>().position = new Vector2(Screen.currentResolution.width + 200, Screen.currentResolution.height + 200);
				upgradePanel.GetComponent<RectTransform> ().position = new Vector2 (Screen.currentResolution.width + 200, Screen.currentResolution.height + 200);
				currentSpot = null;
				placed = true;	
				poppedUp = false;
			}
			else {
				Debug.Log("Not enough money");
			}
		}
		else {
			Debug.Log("Spot is already occupied");
		}
	}

	public void UpgradeTower () {
		towerController = currentSpot.GetComponent<SpotState> ().towerPlaced.GetComponent<TowerController> ();
		if (gameManager.money >= towerController.upgradeCosts[towerController.currLevel - 1]) {
			if (towerController.currLevel < towerController.damages.Length) {
				towerController.currLevel += 1;
				//towerController.currLevel = Mathf.Clamp (towerController.currLevel, 1, towerController.damages.Length);
				gameManager.money -= towerController.upgradeCosts[towerController.currLevel - 1];
				currentSpot = null;
				placed = true;			
				addPanel.GetComponent<RectTransform>().position = new Vector2(Screen.currentResolution.width + 200, Screen.currentResolution.height + 200);
				upgradePanel.GetComponent<RectTransform> ().position = new Vector2 (Screen.currentResolution.width + 200, Screen.currentResolution.height + 200);
				poppedUp = false;
			}
			else {
				Debug.Log("Maximum Level Reached");
			}
		}
		else {
			Debug.Log("Not enough money");
		}
	}
}
