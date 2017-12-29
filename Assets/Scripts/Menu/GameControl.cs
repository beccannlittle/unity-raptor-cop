using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;

public class GameControl : MonoBehaviour {

	public static GameControl control;

	public float playerscore;
	public int numSheepInExistence;
	public Text scoreboardText;
	public Text sheepExistText;
	void Update(){
		QuerySheepRemaining ();
		UpdateScoreUI ();
		UpdateSheepExistUI ();

	}
	// Use this for initialization
	void Awake () {
		if (control == null) {
			DontDestroyOnLoad (gameObject);
			control = this;
		} else if(!control.Equals(this)) {
			Destroy (gameObject);
		}
	}

	public void SaveGameData(){
		BinaryFormatter bf = new BinaryFormatter ();
		FileStream file = File.Create (Application.persistentDataPath + "/savegame.dat");

		GameData optionsData = new GameData ();
		optionsData.score = playerscore;
		optionsData.numExistingSheep = numSheepInExistence;

		bf.Serialize (file, optionsData);
		file.Close ();
	}
	public void LoadGameData(){
		if(File.Exists(Application.persistentDataPath + "/savegame.dat")){
			BinaryFormatter bf = new BinaryFormatter ();
			FileStream file = File.Open (Application.persistentDataPath + "/savegame.dat", FileMode.Open);			
			GameData data = (GameData)bf.Deserialize (file);
			file.Close ();

			playerscore = data.score;
			numSheepInExistence = data.numExistingSheep;
		}
	}
	public void AddScore(float addvalue){
		playerscore += addvalue;
		UpdateScoreUI ();
	}
	public void UpdateScoreUI(){
		scoreboardText.text = "Score: " + playerscore;
	}
	public void AddSheep(int addvalue){
		numSheepInExistence += addvalue;
		UpdateSheepExistUI ();
	}
	public void UpdateSheepExistUI(){
		sheepExistText.text = "Sheep Remaining: " + numSheepInExistence;
	}
	public void QuerySheepRemaining(){
		GameObject[] listOfSheepRemaining = GameObject.FindGameObjectsWithTag ("Sheep");
		numSheepInExistence = listOfSheepRemaining.Length;

	}
}

[Serializable]
class GameData {
	public float score;
	public int numExistingSheep;
}
