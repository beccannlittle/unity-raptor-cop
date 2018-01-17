using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LoadAndNew : MonoBehaviour {
	public string loadCode;
	public static string GlobalLoad;
	public GameObject LoadError;


	public void NewGame(){
		if (File.Exists (GameConstants.RESOURCE_SAVEGAME_PATH)) {
			File.Delete (GameConstants.RESOURCE_SAVEGAME_PATH);
		}
		SceneManager.LoadScene (GameConstants.SCENE_WORLD);
	}

	public void LoadGame(){
		if (File.Exists(GameConstants.RESOURCE_SAVEGAME_PATH)) {
			SceneManager.LoadScene (GameConstants.SCENE_WORLD);
		} else {
			LoadError.SetActive (true);
		}
	}

	public void LoadMainMenu(){
		SceneManager.LoadScene (GameConstants.SCENE_TITLEMENU);
	}

	public void QuitGame(){
		Application.Quit ();
	}
}
