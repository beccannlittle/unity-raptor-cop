using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OptionsMenuActions : MonoBehaviour {
	
	public GameObject optionsMenuGraphics;

	private AudioManager audioManager;
	private UIManager uiManager;

	void Awake() {
		GameObject levelController = GameObject.FindGameObjectWithTag ("LevelController");
		audioManager = levelController.GetComponent<AudioManager> ();
		uiManager = levelController.GetComponent<UIManager> ();
	}

	public void ToggleMusic(bool value) {
		audioManager.MuteMusicVolume (value);
	}

	public void CloseOptionsMenu() {
		uiManager.CloseCurrentUI ();
	}



}
