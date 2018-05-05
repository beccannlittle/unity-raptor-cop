using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;
using UnityEngine.SceneManagement;


public abstract class LevelController : MonoBehaviour {

	private ConstantsManager constantsManager;
	public ScoreManager scoreManager;

	void Awake() {
		GameObject levelController = GameObject.FindGameObjectWithTag ("LevelController");
		scoreManager = levelController.GetComponent<ScoreManager> ();
	}

	public abstract LevelData GenerateLevelData ();
	public abstract void LoadLevelData (LevelData leveld);

	public void ClearDataFromList (GameObject objectHolder){
		if (objectHolder != null) {
			foreach (Transform childObject in objectHolder.transform) {
				childObject.tag = "Untagged";
				Destroy (childObject.gameObject);
			}
		}
	}
	public List<SheepData> BuildListSheepData(GameObject sheepOBJHolder) {
		List<SheepData> sheepDataList = new List<SheepData>();
		foreach(Transform sheep in sheepOBJHolder.transform){
			SheepData sd = sheep.gameObject.GetComponent<SheepEnemy> ().BuildSheepData ();
			sheepDataList.Add (sd);
		}
		return sheepDataList;
	}
	public List<BuildingData> BuildListBuildingData(GameObject buildingOBJHolder){
		List<BuildingData> buildingDataList = new List<BuildingData> ();
		foreach(Transform building in buildingOBJHolder.transform){
			BuildingData bd = building.gameObject.GetComponent<Building> ().BuildBuildingData ();

			buildingDataList.Add (bd);
		}
		return buildingDataList;
	}

	public void LoadBonusLevel(LevelData leveldata,GameObject sheepPrefab, GameObject buildingPrefab){
		//Instantiates new objects based on the loaded LevelData.
		BonusLevelData thisLevelData = (BonusLevelData) leveldata;
		Debug.Log ("Loading Game (JSON):" + JsonUtility.ToJson (thisLevelData));
		if (thisLevelData != null) {
			LoadSavedPlayer (thisLevelData.playerdata);
			LoadListSheepData (thisLevelData.sheepdata, sheepPrefab);

			scoreManager.setPlayerScore (thisLevelData.score);
		}
	}
	public void LoadCombatScene(LevelData leveldata,GameObject sheepPrefab, GameObject buildingPrefab){
		//Instantiates new objects based on the loaded LevelData.
		CombatLevelData thisLevelData = (CombatLevelData) leveldata;
		Debug.Log ("Loading Game (JSON):" + JsonUtility.ToJson (thisLevelData));
		if (thisLevelData != null) {
			LoadSavedPlayer (thisLevelData.playerdata);
			LoadListSheepData (thisLevelData.sheepdata, sheepPrefab);
			LoadListBuildingData (thisLevelData.buildingdata, buildingPrefab);

			scoreManager.setPlayerScore (thisLevelData.score);
		}
	}
	public void LoadSavedPlayer(PlayerData pd){
		if (pd != null) {
			GameObject player = GameObject.FindGameObjectWithTag ("Player");
			if (player != null) {
				player.GetComponent<Player> ().LoadPlayerData (pd);
			}
		}
	}

	public void LoadListSheepData(List<SheepData> sheepdatalist, GameObject prefab){
		GameObject enemyList = GameObject.FindGameObjectWithTag ("EnemyList");
		if (enemyList != null && sheepdatalist != null) {
			int i = 0;
			foreach (SheepData sd in sheepdatalist) {
				GameObject newSheep = Instantiate (prefab, sd.getPositionVector3(), sd.getRotationQuaternion(), enemyList.transform);
				newSheep.GetComponent<StateCartridgeController> ().state = sd.sheepstate;
				i++;
			}
		}
	}

	public void LoadListBuildingData(List<BuildingData> buildingdatalist, GameObject prefab){
		GameObject buildingList = GameObject.FindGameObjectWithTag ("BuildingList");
		if(buildingList != null && buildingdatalist != null) {
			int i = 0;
			foreach(BuildingData bd in buildingdatalist){
				GameObject buildingOBJ = Instantiate (prefab, bd.getPositionVector3(), bd.getRotationQuaternion(), buildingList.transform);
				buildingOBJ.GetComponent<Building> ().m_health.val = bd.health;
				i++;
			}
		}
	}

	public void ClearCombatSceneData(GameObject sheepOBJHolder, GameObject buildingOBJHolder) {
		ClearDataFromList (sheepOBJHolder);
		ClearDataFromList (buildingOBJHolder);
	}
	//PauseScene,ResumeScene
}

[Serializable]
public abstract class LevelData {
	//Generic Level Data not including the scene specific things.
	public string sceneName;
	public float score;
}

[Serializable]
public class CombatLevelData : LevelData {
	public PlayerData playerdata;
	public List<SheepData> sheepdata;
	public List<BuildingData> buildingdata;

}
[Serializable]
public class BonusLevelData : LevelData {
	public PlayerData playerdata;
	public List<SheepData> sheepdata;

}