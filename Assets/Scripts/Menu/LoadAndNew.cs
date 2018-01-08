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
		SceneManager.LoadScene (worldName);
	}

	public void LoadGame(){
		if (File.Exists(Application.persistentDataPath + "/savegame.data")) {
			SceneManager.sceneLoaded += OnSceneLoaded;
			SceneManager.LoadScene (worldName);
		} else {
			LoadError.SetActive (true);
		}
	}
	void OnSceneLoaded(Scene scene, LoadSceneMode mode){
		if (scene.name == "DavidsPlayground") {
			GameObject[] rootObjects = scene.GetRootGameObjects ();
			foreach(GameObject gobj in rootObjects){
				if(gobj.name == "GameManager"){
					gobj.GetComponent<GameControl> ().LoadGameData ();
					break;
				}
			}
		}
	}
	public void LoadMainMenu(){
		SceneManager.LoadScene (titleSceneName);
	}

	public void QuitGame(){
		Application.Quit ();
	}
}
