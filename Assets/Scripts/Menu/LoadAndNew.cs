using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LoadAndNew : MonoBehaviour {
	public string fileName = "savegame.data";
	private string worldName = "DavidsPlayground";
	private string titleSceneName = "TitleScreen";
	public string loadCode;
	public static string GlobalLoad;
	public GameObject LoadError;


	public void NewGame(){
		if (File.Exists (Application.persistentDataPath + fileName)) {
			File.Delete (Application.persistentDataPath + fileName);
		}
		SceneManager.LoadScene (worldName);
	}

	public void LoadGame(){
		if (File.Exists(Application.persistentDataPath + fileName)) {
			SceneManager.LoadScene (worldName);
		} else {
			LoadError.SetActive (true);
		}
	}

	public void LoadMainMenu(){
		SceneManager.LoadScene (titleSceneName);
	}

	public void QuitGame(){
		Application.Quit ();
	}
}
