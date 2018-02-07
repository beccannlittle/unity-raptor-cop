using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class TitleMenuActions : MonoBehaviour {
	
	public string loadCode;
	public static string GlobalLoad;
	public GameObject LoadError;

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
				Instantiate (LoadError, gameObject.transform);
				isLoadError = true;
			}
		}
	}

	public void QuitGame(){
		Application.Quit ();
	}
}
