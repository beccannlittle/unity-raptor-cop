﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class OptionsMenu : MonoBehaviour {
	public GameObject escapeMenuPanel;

	public void ToggleOptionsMenuVisibility(bool shouldSaveOptions){
		if(shouldSaveOptions){
			SaveOptions ();
		}
		gameObject.SetActive (!gameObject.activeSelf);
		escapeMenuPanel.SetActive (true);
	}
	public void SaveOptions(){
		Debug.Log ("[GameManager]: Saving the game");
		//Options newoptions;
		//gameManager.GetComponent<Options> ().SaveOptions (newoptions);

	}
	public void ToggleMuteAll(bool value){
		if (value) {
			AudioListener.volume = 1f;
		} else {
			AudioListener.volume = 0f;
		}
	}
}
