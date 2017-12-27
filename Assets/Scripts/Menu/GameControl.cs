using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;

public class GameControl : MonoBehaviour {

	public static GameControl control;

	public float playerscore;
	public int numSheepInExistence;


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
		GUI.Label (new Rect (10, 10, 100, 30), "Score: " + playerscore);
		GUI.Label (new Rect (10, 10, 150, 30), "Score: " + numSheepInExistence);
	}

	public void SaveOptions(){
		BinaryFormatter bf = new BinaryFormatter ();
		FileStream file = File.Create (Application.persistentDataPath + "/savegame.dat");

		GameData optionsData = new GameData ();
		optionsData.score = playerscore;
		optionsData.numExistingSheep = numSheepInExistence;

		bf.Serialize (file, optionsData);
		file.Close ();
	}
	public void LoadOptions(){
		if(File.Exists(Application.persistentDataPath + "/savegame.dat")){
			BinaryFormatter bf = new BinaryFormatter ();
			FileStream file = File.Open (Application.persistentDataPath + "/savegame.dat", FileMode.Open);			
			GameData data = (GameData)bf.Deserialize (file);
			file.Close ();

			playerscore = data.score;
			numSheepInExistence = data.numExistingSheep;
		}
	}
}

[Serializable]
class GameData {
	public float score;
	public int numExistingSheep;
}
