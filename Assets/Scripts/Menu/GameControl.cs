using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;

public class GameControl : MonoBehaviour {

	public static GameControl control;

	public float playerHealth;

	// Use this for initialization
	void Awake () {
		if (control == null) {
			DontDestroyOnLoad (gameObject);
			control = this;
		} else if(!control.Equals(this)) {
			Destroy (gameObject);
		}
	}
	
	// Update is called once per frame
	void OnGUI () {
		GUI.Label (new Rect (10, 10, 100, 30), "Health: " + playerHealth);
	}

	public void SaveOptions(){
		BinaryFormatter bf = new BinaryFormatter ();
		FileStream file = File.Create (Application.persistentDataPath + "/gameOptions.dat");

		GameOptionsData optionsData = new GameOptionsData ();
		optionsData.playerHealth = playerHealth;

		bf.Serialize (file, optionsData);
		file.Close ();
	}
	public void LoadOptions(){
		if(File.Exists(Application.persistentDataPath + "/gameOptions.dat")){
			BinaryFormatter bf = new BinaryFormatter ();
			FileStream file = File.Open (Application.persistentDataPath + "/gameOptions.dat", FileMode.Open);			
			GameOptionsData data = (GameOptionsData)bf.Deserialize (file);
			file.Close ();

			playerHealth = data.playerHealth;
		}
	}
}

[Serializable]
class GameOptionsData {
	public float playerHealth;
}
