using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using System;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;
using UnityEngine.SceneManagement;

public class SaveLoadGame : MonoBehaviour {
	private ConstantsManager constantsManager;
	void Awake() {
		GameObject gameController = GameObject.FindGameObjectWithTag ("GameController");
		constantsManager = gameController.GetComponent<ConstantsManager> ();
		LoadFromFile ();
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
			Debug.Log ("Saving '"+sgd.saveName+"' (JSON): " + JsonUtility.ToJson (sgd));
			string sceneName = SceneManager.GetActiveScene ().name;
			Debug.Log ("......Saving LvlData of "+sceneName+"(JSON): " + JsonUtility.ToJson (sgd.levelDict[sceneName]));
		} else {
			Debug.LogError ("Need a ConstantsManager.SaveGamePath in order to save.");
		}
	}
	 
	//public void LoadFromFile(string sceneName = default(string)){
	public void LoadFromFile(){
		if (doesSaveFileExist ()) {
			//Load AppData
			BinaryFormatter bf = new BinaryFormatter ();
			FileStream fs = File.Open (constantsManager.getSaveGamePath (), FileMode.Open);
			SaveGameData sgd = (SaveGameData)bf.Deserialize (fs);
			Debug.Log ("Loading the game(JSON): " + JsonUtility.ToJson (sgd));

			LoadApplicationData ();

			//Is there level Data for this scene?
			string sceneName = SceneManager.GetActiveScene ().name;
			if (sgd.levelDict.ContainsKey (sceneName)) {
				LoadLevelData (sgd.levelDict [sceneName]);
				Debug.Log ("Loading the game of "+sceneName+"(JSON) LvlData: " + JsonUtility.ToJson (sgd.levelDict [sceneName]));
			} else {
				Debug.Log ("The sceneName: "+sceneName);
				List<string> keyList = new List<string>(sgd.levelDict.Keys);
				foreach (string s in keyList) {
					Debug.Log ("The Key Value: "+s);

				}
			}
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
		sgData.levelDict[levData.sceneName] = levData;

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
