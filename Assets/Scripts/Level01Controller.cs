using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;
using UnityEngine.SceneManagement;

public class Level01Controller : LevelController {
	public GameObject sheepPrefab;
	public GameObject buildingPrefab;
	public GameObject playerOBJ;
	public GameObject sheepOBJHolder;
	public GameObject buildingOBJHolder;

	public override LevelData GenerateLevelData(){
		Debug.Log ("In the Level01 GenerateLevelData");
		Level01Data levelData = new Level01Data ();
		levelData.sceneName = SceneManager.GetActiveScene ().name;
		levelData.score = scoreManager.getPlayerScore();
		levelData.playerdata = playerOBJ.GetComponent<Player>().BuildPlayerData ();
		levelData.sheepdata = BuildListSheepData ();
		levelData.buildingdata = BuildListBuildingData ();
		Debug.Log ("Score" + scoreManager.getPlayerScore ());
		Debug.Log ("Saving Score" + levelData.score);
		return levelData;
	}

	public override void LoadLevelData(LevelData leveld) {
		Level01Data data = (Level01Data)leveld;
		ClearSceneData ();
		if (data != null) {
			LoadGameControlData (data);
		}
		scoreManager.QuerySheepRemaining ();
	}
	public void ClearSceneData() {
		if (sheepOBJHolder != null) {
			foreach (Transform sheep in sheepOBJHolder.transform) {
				sheep.tag = "Untagged";
				Destroy (sheep.gameObject);
			}
		}
		if (buildingOBJHolder != null) {
			foreach (Transform building in buildingOBJHolder.transform) {
				Destroy (building.gameObject);
			}
		}
	}
	public void LoadGameControlData(Level01Data leveldata){
		Debug.Log ("Loading Game (JSON):" + JsonUtility.ToJson (leveldata));
		if (leveldata != null) {
			LoadSavedPlayer (leveldata.playerdata);
			LoadListSheepData (leveldata.sheepdata);
			LoadListBuildingData (leveldata.buildingdata);

			scoreManager.setPlayerScore (leveldata.score);
		}
	}

	private List<SheepData> BuildListSheepData(){
		List<SheepData> sheepDataList = new List<SheepData>();
		foreach(Transform sheep in sheepOBJHolder.transform){
			SheepData sd = sheep.gameObject.GetComponent<SheepEnemy> ().BuildSheepData ();
			sheepDataList.Add (sd);
		}
		return sheepDataList;
	}

	private List<BuildingData> BuildListBuildingData(){
		List<BuildingData> buildingDataList = new List<BuildingData> ();
		foreach(Transform building in buildingOBJHolder.transform){
			BuildingData bd = building.gameObject.GetComponent<Building> ().BuildBuildingData ();

			buildingDataList.Add (bd);
		}
		return buildingDataList;
	}

	private void LoadSavedPlayer(PlayerData pd){
		if (pd != null) {
			GameObject player = GameObject.FindGameObjectWithTag ("Player");
			if (player != null) {
				player.GetComponent<Player> ().LoadPlayerData (pd);
			}
		}
	}

	private void LoadListSheepData(List<SheepData> sheepdatalist){
		GameObject enemyList = GameObject.FindGameObjectWithTag ("EnemyList");
		if (enemyList != null && sheepdatalist != null) {
			foreach (SheepData sd in sheepdatalist) {
				GameObject newSheep = Instantiate (sheepPrefab, sd.getPositionVector3(), sd.getRotationQuaternion(), enemyList.transform);
				newSheep.GetComponent<StateCartridgeController> ().state = sd.sheepstate;
			}
		}
	}

	private void LoadListBuildingData(List<BuildingData> buildingdatalist){
		GameObject buildingList = GameObject.FindGameObjectWithTag ("BuildingList");
		if(buildingList != null && buildingdatalist != null) {
			foreach(BuildingData bd in buildingdatalist){
				GameObject buildingOBJ = Instantiate (buildingPrefab, bd.getPositionVector3(), bd.getRotationQuaternion(), buildingList.transform);
				buildingOBJ.GetComponent<Building> ().m_health.val = bd.health;
			}
		}
	}
}
[Serializable]
public class Level01Data : LevelData {
	public PlayerData playerdata;
	public List<SheepData> sheepdata;
	public List<BuildingData> buildingdata;
	
}
