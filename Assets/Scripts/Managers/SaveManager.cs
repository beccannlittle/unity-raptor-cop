using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;
using UnityEngine.SceneManagement;

public class SaveManager : MonoBehaviour {

	public GameObject sheepPrefab;
	public GameObject buildingPrefab;
	public GameObject playerOBJ;
	public GameObject sheepOBJHolder;
	public GameObject buildingOBJHolder;

	private ConstantsManager constantsManager;
	private string saveGamePath;
	private ScoreManager scoreManager;

	void Awake() {
		GameObject gameController = GameObject.FindGameObjectWithTag ("GameController");
		constantsManager = gameController.GetComponent<ConstantsManager> ();
		saveGamePath = constantsManager.getSaveGamePath();
		GameObject levelController = GameObject.FindGameObjectWithTag ("LevelController");
		scoreManager = levelController.GetComponent<ScoreManager> ();
		if (File.Exists (saveGamePath)) {
			LoadGameData ();
		}
	}

	// Save //

	public void SaveGameData(){
		Debug.Log ("saving");
		BinaryFormatter bf = new BinaryFormatter ();
		FileStream file = File.Create (saveGamePath);

		SaveGameControlData (bf, file);

		file.Close ();
	}

	public void SaveGameControlData(BinaryFormatter bff, FileStream file){

		GameData optionsData = new GameData ();
		optionsData.score = scoreManager.getPlayerScore();
		optionsData.numExistingSheep = scoreManager.getNumSheepInExistence();

		optionsData.playerdata = BuildPlayerData ();
		optionsData.sheepdata = BuildSheepData ();
		optionsData.buildingdata = BuildBuildingData ();

		bff.Serialize (file, optionsData);
	}

	private PlayerData BuildPlayerData(){
		PlayerData pd = new PlayerData ();
		pd.positionX = playerOBJ.transform.position.x;
		pd.positionY = playerOBJ.transform.position.y;
		pd.positionZ = playerOBJ.transform.position.z;
		pd.rotationX = playerOBJ.transform.rotation.x;
		pd.rotationY = playerOBJ.transform.rotation.y;
		pd.rotationZ = playerOBJ.transform.rotation.z;

		return pd;
	}

	private List<SheepData> BuildSheepData(){
		List<SheepData> sheepDataList = new List<SheepData>();

		foreach(Transform sheep in sheepOBJHolder.transform){
			SheepData sd = new SheepData ();
			sd.positionX = sheep.transform.position.x;
			sd.positionY = sheep.transform.position.y;
			sd.positionZ = sheep.transform.position.z;
			sd.rotationX = sheep.transform.rotation.x;
			sd.rotationY = sheep.transform.rotation.y;
			sd.rotationZ = sheep.transform.rotation.z;
			sd.sheepstate = sheep.GetComponent<StateCartridgeController> ().state;

			sheepDataList.Add (sd);
		}
		return sheepDataList;
	}

	private List<BuildingData> BuildBuildingData(){
		List<BuildingData> buildingDataList = new List<BuildingData> ();
		foreach(Transform building in buildingOBJHolder.transform){
			BuildingData bd = new BuildingData ();

			bd.positionX = building.transform.position.x;
			bd.positionY = building.transform.position.y;
			bd.positionZ = building.transform.position.z;

			bd.rotationX = building.transform.rotation.x;
			bd.rotationY = building.transform.rotation.y;
			bd.rotationZ = building.transform.rotation.z;

			buildingDataList.Add (bd);
		}
		return buildingDataList;
	}

	// Load //

	public bool doesSaveFileExist() {
		return File.Exists (saveGamePath);
	}

	public void LoadGameData(){
		if(File.Exists(saveGamePath)){
			ClearSceneData ();
			BinaryFormatter bf = new BinaryFormatter ();
			FileStream file = File.Open (saveGamePath, FileMode.Open);			

			GameData data = (GameData)bf.Deserialize (file);
			LoadGameControlData (data);
			file.Close ();
		}
	}

	private void ClearSceneData(){
		if (sheepOBJHolder != null) {
			foreach (Transform sheep in sheepOBJHolder.transform) {
				Destroy (sheep.gameObject);
			}
		}
		if (buildingOBJHolder != null) {
			foreach (Transform building in buildingOBJHolder.transform) {
				Destroy (building.gameObject);
			}
		}
	}

	public void ClearSaveData(){
		if(File.Exists(saveGamePath)){
			File.Delete (saveGamePath);
		}
	}

	private void LoadGameControlData(GameData gdata){

		LoadSavedPlayer (gdata.playerdata);
		LoadSavedSheepData (gdata.sheepdata);
		LoadSavedBuildingData (gdata.buildingdata);

		scoreManager.setPlayerScore(gdata.score);
		scoreManager.setNumSheepInExistence(gdata.numExistingSheep);
	}

	private void LoadSavedPlayer(PlayerData pd){
		if(pd != null) {
			GameObject player = GameObject.FindGameObjectWithTag ("Player");
			player.transform.position =  new Vector3(pd.positionX,pd.positionY,pd.positionZ);
			player.transform.rotation = Quaternion.Euler(pd.rotationX,pd.rotationY,pd.rotationZ);
		}
	}

	private void LoadSavedSheepData(List<SheepData> sheepdatalist){
		GameObject enemyList = GameObject.FindGameObjectWithTag ("EnemyList");
		foreach(SheepData sd in sheepdatalist){
			GameObject newSheep = Instantiate (sheepPrefab, new Vector3(sd.positionX,sd.positionY,sd.positionZ), Quaternion.Euler(sd.rotationX,sd.rotationY,sd.rotationZ), enemyList.transform);
			newSheep.GetComponent<StateCartridgeController> ().state = sd.sheepstate;
		}
	}

	private void LoadSavedBuildingData(List<BuildingData> buildingdatalist){
		GameObject buildingList = GameObject.FindGameObjectWithTag ("BuildingList");
		foreach(BuildingData bd in buildingdatalist){
			Instantiate (buildingPrefab, new Vector3(bd.positionX,bd.positionY,bd.positionZ), Quaternion.Euler(bd.rotationX,bd.rotationY,bd.rotationZ), buildingList.transform);
		}
	}

}

[Serializable]
class GameData {
	public string currentLevel;
	public float score;
	public int numExistingSheep;
	public PlayerData playerdata;
	public List<SheepData> sheepdata;
	public List<BuildingData> buildingdata;
}
