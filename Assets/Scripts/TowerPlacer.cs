using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;

public class TowerPlacer : MonoBehaviour {

	public GameObject addPanel;
	public GameObject upgradePanel;
	public GameObject tower;
	public float groundClearence = 0.5f;
	
	private GameManager gameManager;
	private TowerController towerController;
	private GameObject towerSpots;
	private GameObject currentSpot;
	private GameObject currPanel;
	private Transform[] spots;
	private Transform spot;
	private bool inputType;
	private bool poppedUp;
	private bool spotFound;
	private bool placed;
	private bool misclicked;
	private bool notPanels;
	private Vector3 position;
	private Vector3 placePosition;
	private Vector3 cameraCurrPosition;
	private Touch touch;
	private int fingerId;

	void Start() { 
		towerSpots = GameObjectsManager.towerSpots;//GameObject.FindGameObjectWithTag ("Tower spots");
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
				bool UIEvent = false;
				if (Input.touchSupported) {
					UIEvent = EventSystem.current.IsPointerOverGameObject (fingerId);
				}
				else {
					UIEvent = EventSystem.current.IsPointerOverGameObject ();
				}
				if (UIEvent) {
					GameObject currUIElement = EventSystem.current.currentSelectedGameObject;
					if (currUIElement == null) {
						spotFound = true;
						notPanels = false;
					}
					else if ((currUIElement.transform.parent.gameObject == addPanel) || (currUIElement.transform.parent.gameObject == upgradePanel)) {
						spotFound = true;
						notPanels = false;
					}
					else {
						notPanels = true;
					}
				}
				if (!spotFound) {
					if (currentSpot != null) {
						SpotState spotState = currentSpot.GetComponent<SpotState>();
						if (spotState.towerPlaced != null) {
							towerController = spotState.towerPlaced.GetComponent<TowerController> ();
							towerController.projector.SetActive(false);
						}
					}
					currentSpot = null;
				}
				if (!poppedUp) {
					if (spotFound) {
						currentSpot = spot.gameObject;
						SpotState spotState = currentSpot.gameObject.GetComponent<SpotState> ();
						placed = spotState.placed;
						if (placed) {
							upgradePanel.GetComponent<RectTransform> ().position = position;
							currPanel = upgradePanel;
							spotState.towerPlaced.GetComponent<TowerController>().projector.SetActive(true);
						}
						else {
							addPanel.GetComponent<RectTransform> ().position = position;
							currPanel = addPanel;
						}
						placePosition = spot.transform.position;
						//Debug.DrawLine (ray.origin, hit.point);
						poppedUp = true;
						misclicked = false;						
					} else {
						currentSpot = null;
					}
				} else {
					if (UIEvent) {
						if (notPanels) {
							misclicked = true;
						} 
						else {
							misclicked = false;
						}
					}
					if (misclicked) { /*&& urrentSpot != spot.gameObject)*/
						if (currentSpot != spot.gameObject) {
							addPanel.GetComponent<RectTransform> ().position = new Vector2 (Screen.currentResolution.width + 200, Screen.currentResolution.height + 200);
							upgradePanel.GetComponent<RectTransform> ().position = new Vector2 (Screen.currentResolution.width + 200, Screen.currentResolution.height + 200);
							if (currentSpot != null) {
								SpotState spotState = currentSpot.GetComponent<SpotState>();
								if (spotState.towerPlaced != null) {
									towerController = spotState.towerPlaced.GetComponent<TowerController> ();
									towerController.projector.SetActive(false);
								}
							}
							currPanel = null;
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
				currPanel.GetComponent<RectTransform> ().position = Camera.main.WorldToScreenPoint(spot.transform.position);
			}
			cameraCurrPosition = Camera.main.transform.position;
		}
	}

	public void PlaceTower() {
		if (!placed) {
			if (gameManager.money >= towerController.upgradeCosts[towerController.currLevel - 1]) { 	
				gameManager.money -= towerController.upgradeCosts[towerController.currLevel - 1];
				GameObject newTower = Instantiate (tower, new Vector3 (placePosition.x, placePosition.y + groundClearence, placePosition	.z), new Quaternion()) as GameObject;
				GameObjectsManager.towers.Add(newTower);
				//GameObject[] towersArr = GameObject.FindGameObjectsWithTag("Tower");
				SpotState spotState = currentSpot.GetComponent<SpotState>();
				spotState.placed = true;	
				spotState.towerPlaced = newTower.gameObject;
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
		SpotState spotState = currentSpot.GetComponent<SpotState>();
		towerController = spotState.towerPlaced.GetComponent<TowerController> ();
		if (gameManager.money >= towerController.upgradeCosts[towerController.currLevel - 1]) {
			if (towerController.currLevel < towerController.damages.Length) {
				towerController.currLevel += 1;
				towerController.gameObject.GetComponent<SphereCollider>().radius = towerController.radiuses[towerController.currLevel - 1];
				towerController.projector.GetComponent<Projector>().orthographicSize = towerController.radiuses[towerController.currLevel - 1];
				towerController.projector.SetActive(false);
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
