using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using System;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;

public class SaveGame : MonoBehaviour {
	private ConstantsManager constantsManager;
	void Awake() {
		GameObject gameController = GameObject.FindGameObjectWithTag ("GameController");
		constantsManager = gameController.GetComponent<ConstantsManager> ();
		if (doesSaveFileExist ()) {
			LoadFromFile ();
		}
	}
	public bool doesSaveFileExist() {
		return File.Exists (constantsManager.getSaveGamePath());
	}

	public void ClearSaveData(){
		if(doesSaveFileExist()){
			File.Delete (constantsManager.getSaveGamePath());
		}
	}

	public void SaveToFile(){
		if (constantsManager.getSaveGamePath () != null) {
			ClearSaveData ();
			BinaryFormatter bf = new BinaryFormatter ();
			FileStream file = File.Create (constantsManager.getSaveGamePath ());

			SaveGameData sgd = new SaveGameData ();
			AddApplicationData (sgd);
			sgd.saveName = "OnlyOneSave";
			AddLevelData (sgd);
			bf.Serialize (file, sgd);
			file.Close ();
			Debug.Log ("Saving the game(JSON): " + JsonUtility.ToJson (sgd));
			Debug.Log ("Saving the game(JSON) LvlData: " + JsonUtility.ToJson (sgd.levelDict["Level01"]));
		} else {
			Debug.LogError ("Need a ConstantsManager.SaveGamePath in order to save.");
		}
	}

	//public void LoadFromFile(string sceneName = default(string)){
	public void LoadFromFile(){
		if(doesSaveFileExist()){
			//Load AppData
			BinaryFormatter bf = new BinaryFormatter();
			FileStream fs = File.Open (constantsManager.getSaveGamePath (), FileMode.Open);
			SaveGameData sgd = (SaveGameData)bf.Deserialize (fs);
			LoadApplicationData ();
			if (sgd.levelDict.Count > 0) {
				LoadLevelData (sgd.levelDict ["Level01"]);
			}
			Debug.Log ("Loading the game(JSON): " + JsonUtility.ToJson (sgd));
			Debug.Log ("Loading the game(JSON) LvlData: " + JsonUtility.ToJson (sgd.levelDict["Level01"]));
			/*
			if (!string.IsNullOrEmpty(sceneName)) {
			//Look for levelData with this sceneName
			} else {
				Debug.Log ("No sceneName provided to SaveGame.LoadFromFile() |"+sceneName+"|");
			}
			*/
		}
	}

	public void AddApplicationData(SaveGameData sgData){
		GameController gameController = GameObject.FindWithTag("GameController").GetComponent<GameController>();
		ApplicationData aData = gameController.GenerateApplicationData();
		sgData.appData = aData;
	}
	public void LoadApplicationData(){
		GameController gameController = GameObject.FindWithTag("GameController").GetComponent<GameController>();
		gameController.LoadApplicationData();
	}

	public void AddLevelData(SaveGameData sgData){
		LevelController levContrl = (LevelController) GameObject.FindWithTag ("LevelController").GetComponent (typeof(LevelController));
		LevelData levData = levContrl.GenerateLevelData();
		sgData.levelDict.Add("Level01",levData);

	}
	public void LoadLevelData(LevelData lData){
		LevelController levContrl = (LevelController)GameObject.FindWithTag ("LevelController").GetComponent (typeof(LevelController));
		if (levContrl != null) {
			levContrl.LoadLevelData (lData);
		} else {
			Debug.Log ("LevelController exists but does not have a LevelController script. This could be an unplayable scene.");
		}
	}
}

[Serializable]
public class SaveGameData {
	public ApplicationData appData;
	public Dictionary <string,LevelData> levelDict = new Dictionary<string,LevelData>();
	public string saveName;

}
