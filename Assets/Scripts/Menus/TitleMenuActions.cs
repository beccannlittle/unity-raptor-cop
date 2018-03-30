using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class TitleMenuActions : MonoBehaviour {

	public GameObject loadError;
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

	public void NewGame(){
		saveManager.ClearSaveData ();
		SceneManager.LoadScene(constantsManager.getLevel(0));
	}

	private bool isLoadError;
	public void LoadGame() {
		if (saveManager.doesSaveFileExist()) {
			SceneManager.LoadScene (constantsManager.getLevel(0));
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
		if(optionsMenuGraphics != null){
			uiManager.SetCurrentUI (optionsMenuGraphics);
		}
	}
}
