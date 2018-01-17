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
		if (File.Exists (GameConstants.Resource_SaveGamePath)) {
			File.Delete (GameConstants.Resource_SaveGamePath);
		}
		SceneManager.LoadScene (GameConstants.Scene_World);
	}

	public void LoadGame(){
		if (File.Exists(GameConstants.Resource_SaveGamePath)) {
			SceneManager.LoadScene (GameConstants.Scene_World);
		} else {
			LoadError.SetActive (true);
		}
	}

	public void LoadMainMenu(){
		SceneManager.LoadScene (GameConstants.Scene_TitleMenu);
	}

	public void QuitGame(){
		Application.Quit ();
	}
}
