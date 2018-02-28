using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EscapeMenuActions : MonoBehaviour {

	public GameObject gameManager;
	public GameObject escapeMenuGraphics;
	public GameObject optionsMenuGraphics;

	private GameObject levelController;
	private UIManager uiManager;

	void Awake() {
		levelController = GameObject.FindGameObjectWithTag ("LevelController");
		uiManager = levelController.GetComponent<UIManager> ();
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
