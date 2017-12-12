using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LoadAndNew : MonoBehaviour {
	public string fileName = "savegame.data";
	public string loadCode;
	public static string GlobalLoad;
	public GameObject LoadError;

	public void Start(){
		try{
			StreamReader ourFile = new StreamReader (fileName);
		
			string line = ourFile.ReadLine ();
			while(line != null){
				loadCode = line;
				line = ourFile.ReadLine ();
			}
			ourFile.Close ();
		} catch {
			StreamWriter writer = new StreamWriter (fileName,true);
			writer.Close ();
		}
	}

	public void NewGame(){
		GlobalLoad = null;
		SceneManager.LoadScene ("Playground");
	}

	public void LoadGame(){
		GlobalLoad = loadCode;
		if (GlobalLoad == "SavingTheGame") {
			SceneManager.LoadScene ("Playground");
		} else {
			LoadError.SetActive (true);
		}
	}
	public void LoadMainMenu(){
		SceneManager.LoadScene ("TitleScreen");
	}

	public void QuitGame(){
		Application.Quit ();
	}
}
