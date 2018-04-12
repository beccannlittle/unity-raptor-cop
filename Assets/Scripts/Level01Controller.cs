using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;

public class Level01Controller : LevelController {
	private ScoreManager scoreManager;



	new public void LoadLevelData(LevelData leveld) {
		Level01Data data = (Level01Data)leveld;
		ClearSceneData ();
		LoadGameControlData (data);
		scoreManager.QuerySheepRemaining ();
	}
}
[Serializable]
public class Level01Data : LevelData {
	public PlayerData playerdata;
	public List<SheepData> sheepdata;
	public List<BuildingData> buildingdata;
	
}
