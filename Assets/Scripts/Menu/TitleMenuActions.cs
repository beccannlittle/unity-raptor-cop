using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class TitleMenuActions : MonoBehaviour {

	public GameObject loadError;
	public GameObject optionsMenuGraphics;

	private GameObject gameController;
	private UIManager uiManager;

	void Awake() {
		gameController = GameObject.FindGameObjectWithTag ("GameController");
		uiManager = gameController.GetComponent<UIManager> ();
	}

	public void NewGame(){
		if (File.Exists (GameConstants.RESOURCE_SAVEGAME_PATH)) {
			File.Delete (GameConstants.RESOURCE_SAVEGAME_PATH);
		}
		SceneManager.LoadScene (GameConstants.SCENE_WORLD_001);
	}

	private bool isLoadError;
	public void LoadGame(){
		if (File.Exists(GameConstants.RESOURCE_SAVEGAME_PATH)) {
			SceneManager.LoadScene (GameConstants.SCENE_WORLD_001);
		} else {
			if (!isLoadError) {
				Instantiate (loadError, gameObject.transform);
				isLoadError = true;
			}
		}
	}

	public void QuitGame(){
		Application.Quit ();
	}

	public void OpenOptionsMenu() {
		uiManager.SetCurrentUI (optionsMenuGraphics);
	}
}
