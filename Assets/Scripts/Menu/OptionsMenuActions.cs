using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OptionsMenuActions : MonoBehaviour {
	
	public GameObject optionsMenuGraphics;

	private GameObject levelController;
	private UIManager uiManager;

	void Awake() {
		levelController = GameObject.FindGameObjectWithTag ("LevelController");
	}

	public void ToggleMusic(bool value) {
		levelController.GetComponent<AudioManager> ().MuteMusicVolume (value);
	}

	public void CloseOptionsMenu() {
		levelController.GetComponent<UIManager> ().CloseCurrentUI ();
	}



}
