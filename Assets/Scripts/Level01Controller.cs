using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;

public class Level01Controller : MonoBehaviour,LevelController {

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
	}
	public LevelData GenerateLevelData(){
		Debug.Log ("Generating Level01 LevelData");
		Level01Data levelData = new Level01Data ();
		levelData.sceneName = "test";
		levelData.score = scoreManager.getPlayerScore();
		levelData.playerdata = BuildPlayerData ();
		levelData.sheepdata = BuildSheepData ();
		levelData.buildingdata = BuildBuildingData ();
		Debug.Log ("Score" + scoreManager.getPlayerScore ());
		Debug.Log ("Saving Score" + levelData.score);
		return levelData;
	}

	private PlayerData BuildPlayerData(){
		Debug.Log ("Build Player Data PlayerPos="+playerOBJ.transform.position.x+","+playerOBJ.transform.position.y+","+playerOBJ.transform.position.z);
		Debug.Log ("Build Player Data PlayerRot="+playerOBJ.transform.rotation.x+","+playerOBJ.transform.rotation.y+","+playerOBJ.transform.rotation.z+","+playerOBJ.transform.rotation.w);
		PlayerData pd = new PlayerData ();
		pd.positionX = playerOBJ.transform.position.x;
		pd.positionY = playerOBJ.transform.position.y;
		pd.positionZ = playerOBJ.transform.position.z;
		pd.rotationX = playerOBJ.transform.rotation.x;
		pd.rotationY = playerOBJ.transform.rotation.y;
		pd.rotationZ = playerOBJ.transform.rotation.z;
		pd.rotationW = playerOBJ.transform.rotation.w;

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
			//Health
			bd.health = building.GetComponent<Building> ().m_health.val;
			//Location
			bd.positionX = building.transform.position.x;
			bd.positionY = building.transform.position.y;
			bd.positionZ = building.transform.position.z;
			//Rotation
			bd.rotationX = building.transform.rotation.x;
			bd.rotationY = building.transform.rotation.y;
			bd.rotationZ = building.transform.rotation.z;

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

	private void ClearSceneData() {
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
	private void LoadGameControlData(Level01Data leveldata){
		Debug.Log ("Loading Game (JSON):"+JsonUtility.ToJson(leveldata));
		LoadSavedPlayer (leveldata.playerdata);
		LoadSavedSheepData (leveldata.sheepdata);
		LoadSavedBuildingData (leveldata.buildingdata);

		scoreManager.setPlayerScore(leveldata.score);
	}

	private void LoadSavedPlayer(PlayerData pd){
		
		if (pd != null) {
			GameObject player = GameObject.FindGameObjectWithTag ("Player");
			Debug.Log ("Load Saved Player Data");
			if (player != null) {
				player.transform.position = new Vector3 (pd.positionX, pd.positionY, pd.positionZ);
				player.transform.rotation = new Quaternion (pd.rotationX, pd.rotationY, pd.rotationZ, pd.rotationW);

				Debug.Log ("Load PlayerPos="+player.transform.position.x+","+player.transform.position.y+","+player.transform.position.z);
				Debug.Log ("Load PlayerRot="+player.transform.rotation.x+","+player.transform.rotation.y+","+player.transform.rotation.z+","+player.transform.rotation.w);
			}
		}
	}

	private void LoadSavedSheepData(List<SheepData> sheepdatalist){
		GameObject enemyList = GameObject.FindGameObjectWithTag ("EnemyList");
		if (enemyList != null && sheepdatalist != null) {
			foreach (SheepData sd in sheepdatalist) {
				GameObject newSheep = Instantiate (sheepPrefab, new Vector3 (sd.positionX, sd.positionY, sd.positionZ), Quaternion.Euler (sd.rotationX, sd.rotationY, sd.rotationZ), enemyList.transform);
				newSheep.GetComponent<StateCartridgeController> ().state = sd.sheepstate;
			}
		}
	}

	private void LoadSavedBuildingData(List<BuildingData> buildingdatalist){
		GameObject buildingList = GameObject.FindGameObjectWithTag ("BuildingList");
		if(buildingList != null && buildingdatalist != null) {
			foreach(BuildingData bd in buildingdatalist){
				GameObject buildingOBJ = Instantiate (buildingPrefab, new Vector3(bd.positionX,bd.positionY,bd.positionZ), Quaternion.Euler(bd.rotationX,bd.rotationY,bd.rotationZ), buildingList.transform);
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
