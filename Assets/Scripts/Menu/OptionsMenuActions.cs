using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OptionsMenuActions : MonoBehaviour {
	
	public GameObject optionsMenuGraphics;

	private GameObject gameController;
	private UIManager uiManager;

	void Awake() {
		gameController = GameObject.FindGameObjectWithTag ("GameController");
		uiManager = gameController.GetComponent<UIManager> ();
	}

	public void ToggleMusic(bool value) {
		gameController.GetComponent<AudioManager> ().MuteMusicVolume (value);
	}

	public void CloseOptionsMenu() {
		uiManager.CloseCurrentUI ();
	}



}
