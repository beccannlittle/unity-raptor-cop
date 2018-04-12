using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;


public abstract class LevelController : MonoBehaviour,LevelControllerInterface {
	public GameObject sheepPrefab;
	public GameObject buildingPrefab;
	public GameObject playerOBJ;
	public GameObject sheepOBJHolder;
	public GameObject buildingOBJHolder;

	private ConstantsManager constantsManager;
	private string saveGamePath;
	private ScoreManager scoreManager;

	void Awake() {
		GameObject levelController = GameObject.FindGameObjectWithTag ("LevelController");
		scoreManager = levelController.GetComponent<ScoreManager> ();
	}

	public LevelData GenerateLevelData(){
		Debug.Log ("Generating Level01 LevelData");
		Level01Data levelData = new Level01Data ();
		levelData.sceneName = "test";
		levelData.score = scoreManager.getPlayerScore();
		levelData.playerdata = playerOBJ.GetComponent<Player>().BuildPlayerData ();
		levelData.sheepdata = BuildListSheepData ();
		levelData.buildingdata = BuildListBuildingData ();
		Debug.Log ("Score" + scoreManager.getPlayerScore ());
		Debug.Log ("Saving Score" + levelData.score);
		return levelData;
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

	public bool doesSaveFileExist() {
		return File.Exists (saveGamePath);
	}
	public void LoadLevelData(LevelData leveld){
		Level01Data data = (Level01Data)leveld;
		ClearSceneData ();
		LoadGameControlData (data);
		scoreManager.QuerySheepRemaining ();
	}

	public void LoadGameControlData(Level01Data leveldata){
		Debug.Log ("Loading Game (JSON):"+JsonUtility.ToJson(leveldata));
		LoadSavedPlayer (leveldata.playerdata);
		LoadListSheepData (leveldata.sheepdata);
		LoadListBuildingData (leveldata.buildingdata);

		scoreManager.setPlayerScore(leveldata.score);
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
				GameObject newSheep = Instantiate (sheepPrefab, new Vector3 (sd.positionX, sd.positionY, sd.positionZ), new Quaternion (sd.rotationX, sd.rotationY, sd.rotationZ, sd.rotationW), enemyList.transform);
				newSheep.GetComponent<StateCartridgeController> ().state = sd.sheepstate;
			}
		}
	}

	private void LoadListBuildingData(List<BuildingData> buildingdatalist){
		GameObject buildingList = GameObject.FindGameObjectWithTag ("BuildingList");
		if(buildingList != null && buildingdatalist != null) {
			foreach(BuildingData bd in buildingdatalist){
				GameObject buildingOBJ = Instantiate (buildingPrefab, new Vector3(bd.positionX,bd.positionY,bd.positionZ), new Quaternion (bd.rotationX,bd.rotationY,bd.rotationZ,bd.rotationW), buildingList.transform);
				buildingOBJ.GetComponent<Building> ().m_health.val = bd.health;
			}
		}
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
	//PauseScene,ResumeScene
}
public interface LevelControllerInterface {
	LevelData GenerateLevelData ();
	void LoadLevelData (LevelData ld);
	//PauseScene,ResumeScene
}

[Serializable]
public abstract class LevelData {
	//Generic Level Data not including the scene specific things.
	public string sceneName;
	public float score;
}