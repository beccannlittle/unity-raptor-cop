using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuActions : MonoBehaviour {

	public GameObject gameManager;
	public GameObject escapeMenu;
	public GameObject optionsMenu;

	void Awake() {
		// TODO: Audio options don't work
//		gameManager.GetComponent<GameAudioOptions> ().LoadAudioOptions ();
	}

	void Update () {
		if(Input.GetKeyDown(KeyCode.Escape)){
			if (escapeMenu.activeSelf) {
				Resume ();
			} else if (optionsMenu.activeSelf) {
				ToggleOptionsMenu ();
			} else {
				Pause ();
			}
		}
	}

	private void Pause() {
		escapeMenu.SetActive (true);
	}

	public void Resume() {
		escapeMenu.SetActive (false);
	}

	public void Save() {
		gameManager.GetComponent<GameControl> ().ClearSaveData ();
		gameManager.GetComponent<GameControl> ().SaveGameData ();
		Resume ();
	}

	public void SaveAndQuit() {
		Save ();
		Quit ();
	}

	public void Quit() {
		SceneManager.LoadScene (GameConstants.SCENE_TITLEMENU);	
		Destroy (gameManager);
	}

	public void ToggleOptionsMenu() {
		escapeMenu.SetActive (!escapeMenu.activeSelf);
		optionsMenu.SetActive (!optionsMenu.activeSelf);
	}

//	public void SaveOptions(){
//		Debug.Log ("[GameManager]: Saving the game");
//		gameManager.GetComponent<GameAudioOptions> ().SaveAudioOptions ();
//
//	}
//	public void ToggleMuteAll(bool value){
//		if (value) {
//			AudioListener.volume = 1f;
//		} else {
//			AudioListener.volume = 0f;
//		}
//	}

}
