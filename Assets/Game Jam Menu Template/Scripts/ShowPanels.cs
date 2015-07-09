using UnityEngine;
using System.Collections;

public class ShowPanels : MonoBehaviour {

	public GameObject optionsPanel;							//Store a reference to the Game Object OptionsPanel 
	public GameObject optionsTint;							//Store a reference to the Game Object OptionsTint 
	public GameObject menuPanel;							//Store a reference to the Game Object MenuPanel 
	public GameObject pausePanel;							//Store a reference to the Game Object PausePanel 
	public GameObject pauseButton;
	public GameObject addPanel;
	public GameObject upgradePanel;
	public GameObject timer;

	//Call this function to activate and display the Options panel during the main menu
	public void ShowOptionsPanel()
	{
		optionsPanel.SetActive(true);
		optionsTint.SetActive(true);
	}

	//Call this function to deactivate and hide the Options panel during the main menu
	public void HideOptionsPanel()
	{
		optionsPanel.SetActive(false);
		optionsTint.SetActive(false);
	}

	//Call this function to activate and display the main menu panel during the main menu
	public void ShowMenu()
	{
		menuPanel.SetActive (true);
	}

	//Call this function to deactivate and hide the main menu panel during the main menu
	public void HideMenu()
	{
		menuPanel.SetActive (false);
	}
	
	//Call this function to activate and display the Pause panel during game play
	public void ShowPausePanel()
	{
		pausePanel.SetActive (true);
		optionsTint.SetActive(true);
	}

	//Call this function to deactivate and hide the Pause panel during game play
	public void HidePausePanel()
	{
		pausePanel.SetActive (false);
		optionsTint.SetActive(false);

	}

	public void ShowPauseButton () {
		pauseButton.SetActive (true);
	}

	public void HidePauseButton () {
		pauseButton.SetActive (false);
	}

	public void ShowAddPanel () {
		addPanel.SetActive (true);
	}
	
	public void HideAddPanel () {
		addPanel.SetActive (false);
	}

	public void ShowUpgradePanel () {
		upgradePanel.SetActive (true);
	}
	
	public void HideUpgradePanel () {
		upgradePanel.SetActive (false);
	}

	public void ShowTimer () {
		timer.SetActive (true);
	}
	
	public void HideTimer () {
		timer.SetActive (false);
	}
}
