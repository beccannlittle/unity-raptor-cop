using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;
using UnityEngine.SceneManagement;

public class Level01Controller : LevelController {
	//Level01Controller should manage what/when/how to save/load game data
	//LevelController should provide common tools to save/load game data

	public GameObject sheepPrefab;
	public GameObject buildingPrefab;
	public GameObject playerOBJ;
	public GameObject sheepOBJHolder;
	public GameObject buildingOBJHolder;

	public override LevelData GenerateLevelData(){
		Debug.Log ("In the Level01 GenerateLevelData");
		CombatLevelData levelData = new CombatLevelData ();
		levelData.sceneName = SceneManager.GetActiveScene ().name;
		levelData.score = scoreManager.getPlayerScore();
		levelData.playerdata = playerOBJ.GetComponent<Player>().BuildPlayerData ();
		levelData.sheepdata = BuildListSheepData (sheepOBJHolder);
		levelData.buildingdata = BuildListBuildingData (buildingOBJHolder);
		Debug.Log ("Score" + scoreManager.getPlayerScore ());
		Debug.Log ("Saving Score" + levelData.score);
		return levelData;
	}

	public override void LoadLevelData(LevelData leveld) {
		//We should modify this so that it just moves/deletes the existing sheep/buildings in the scene instead of clear and instantiate new ones
		CombatLevelData data = (CombatLevelData)leveld;
		ClearCombatSceneData (sheepOBJHolder, buildingOBJHolder);
		if (data != null) {
			LoadCombatScene (data,sheepPrefab, buildingPrefab);
		}
		scoreManager.QuerySheepRemaining ();
		scoreManager.QueryBuildingsRemaining ();
	}
}
