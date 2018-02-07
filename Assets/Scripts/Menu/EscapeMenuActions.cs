using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EscapeMenuActions : MonoBehaviour {

	public GameObject gameManager;
	public GameObject escapeMenuGraphics;
	public GameObject optionsMenuGraphics;

	void Update () {
		if(Input.GetKeyDown(KeyCode.Escape)){
			if (escapeMenuGraphics.activeSelf) {
				Resume ();
			} else {
				Pause ();
			}
		}
	}

	private void Pause() {
		escapeMenuGraphics.SetActive (true);
	}

	public void Resume() {
		escapeMenuGraphics.SetActive (false);
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

	public void OpenOptionsMenu() {
		escapeMenuGraphics.SetActive (false);
		optionsMenuGraphics.SetActive (true);
	}

}
