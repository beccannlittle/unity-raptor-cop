using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EscapeMenuActions : MonoBehaviour {

	public GameObject escapeMenuGraphics;
	public GameObject optionsMenuGraphics;

	private ConstantsManager constantsManager;
	private UIManager uiManager;
	private SaveManager saveManager;

	void Awake() {
		GameObject gameController = GameObject.FindGameObjectWithTag ("GameController");
		constantsManager = gameController.GetComponent<ConstantsManager> ();
		GameObject levelController = GameObject.FindGameObjectWithTag ("LevelController");
		uiManager = levelController.GetComponent<UIManager> ();
		saveManager = levelController.GetComponent<SaveManager> ();
	}

	private void Pause() {
		uiManager.SetCurrentUI (escapeMenuGraphics);
	}

	public void Resume() {
		uiManager.CloseCurrentUI ();
	}

	public void Save() {
		Debug.Log (saveManager);
		saveManager.ClearSaveData ();
		saveManager.SaveGameData ();
		Resume ();
	}

	public void SaveAndQuit() {
		Save ();
		Quit ();
	}

	public void Quit() {
		SceneManager.LoadScene (constantsManager.getTitleScreen());	
	}

	public void OpenOptionsMenu() {
		uiManager.SetCurrentUI (optionsMenuGraphics);
	}

}
