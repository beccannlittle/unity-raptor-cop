using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OptionsMenuActions : MonoBehaviour {

	public GameObject levelController;
	public GameObject optionsMenuGraphics;
	[Header("Optional")]
	public GameObject previousMenuGraphics;

	void Update () {
		if(Input.GetKeyDown(KeyCode.Escape)){
			if (optionsMenuGraphics.activeSelf) {
				optionsMenuGraphics.SetActive (false);
			}
		}
	}

	public void ToggleMusic(bool value) {
		levelController.GetComponent<AudioManager> ().MuteMusicVolume (value);
	}

	public void CloseOptionsMenu() {
		optionsMenuGraphics.SetActive (false);
		if (previousMenuGraphics) {
			previousMenuGraphics.SetActive (true);
		}
	}



}
