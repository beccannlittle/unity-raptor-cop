using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EscapeMenuActions : MonoBehaviour {

	public GameObject gameManager;
	public GameObject escapeMenuGraphics;
	public GameObject optionsMenuGraphics;

	private GameObject gameController;
	private UIManager uiManager;

	void Awake() {
		gameController = GameObject.FindGameObjectWithTag ("GameController");
		uiManager = gameController.GetComponent<UIManager> ();
	}

	private bool shouldPause;

	void Update () {
		if (Input.GetKeyDown (KeyCode.Escape)) {
			if (!uiManager.getHasHandledEsc ()) {
				Pause ();
				uiManager.setHasHandledEsc ();
			}
		}
	}

	private void Pause() {
		uiManager.SetCurrentUI (escapeMenuGraphics);
	}

	public void Resume() {
		uiManager.CloseCurrentUI ();
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
		uiManager.SetCurrentUI (optionsMenuGraphics);
	}

}
