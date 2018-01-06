using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;
using UnityEngine.SceneManagement;

public class GameControl : MonoBehaviour {
	public static GameControl control;

	public float playerscore;
	public int numSheepInExistence;
	public Text scoreboardText;
	public Text sheepExistText;

	public GameObject sheepPrefab;
	public GameObject buildingPrefab;
	public GameObject playerOBJ;
	public GameObject sheepOBJHolder;
	public GameObject buildingOBJHolder;

	void Update(){
		QuerySheepRemaining ();
		UpdateScoreUI ();
		UpdateSheepExistUI ();

		if (hasWonGame ()) {
			this.EndGame ();
		}
	}
	// Use this for initialization
	void Awake () {
		if (control == null) {
			DontDestroyOnLoad (gameObject);
			control = this;
			LoadGameData ();
		} else if(!control.Equals(this)) {
			Destroy (gameObject);
		}
	}
	private bool hasWonGame(){
		return (numSheepInExistence <= 0);
	}
	private void EndGame(){
		Debug.Log ("You won the game!");
		SceneManager.LoadScene ("Credits");
		Destroy (this.gameObject);
	}
	public void SaveGameData(){
		BinaryFormatter bf = new BinaryFormatter ();
		FileStream file = File.Create (Application.persistentDataPath + "/savegame.data");

		SaveGameControlData (bf, file);

		file.Close ();
	}
	public void LoadGameData(){
		if(File.Exists(Application.persistentDataPath + "/savegame.data")){
			BinaryFormatter bf = new BinaryFormatter ();
			FileStream file = File.Open (Application.persistentDataPath + "/savegame.data", FileMode.Open);			

			LoadGameControlData ();

			GameData data = (GameData)bf.Deserialize (file);
			file.Close ();

			playerscore = data.score;
			numSheepInExistence = data.numExistingSheep;
			Debug.Log ("PlayerScore: "+playerscore);
			Debug.Log ("SheepInExistence: "+numSheepInExistence);
		}
	}
	public void ClearSaveData(){
		if(File.Exists(Application.persistentDataPath + "/savegame.data")){
			File.Delete (Application.persistentDataPath + "/savegame.data");
		}
	}
	public void AddScore(float addvalue){
		playerscore += addvalue;
		UpdateScoreUI ();
	}
	public void UpdateScoreUI(){
		if (scoreboardText != null) {
			scoreboardText.text = "Score: " + playerscore;
		}
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
	public void SaveGameControlData(BinaryFormatter bff, FileStream file){

		GameData optionsData = new GameData ();
		optionsData.score = playerscore;
		optionsData.numExistingSheep = numSheepInExistence;
		optionsData.playerdata = BuildPlayerData ();
		optionsData.sheepdata = BuildSheepData ();
		optionsData.buildingdata = BuildBuildingData ();

		bff.Serialize (file, optionsData);
	}
	public void LoadGameControlData(){
	}
	private PlayerData BuildPlayerData(){
		PlayerData pd = new PlayerData ();
		pd.transform = playerOBJ.transform;

		return pd;
	}
	private void LoadSavedPlayer(PlayerData pdata){
		if(pdata != null) {
			playerOBJ.transform.position = pdata.transform.position;
			playerOBJ.transform.rotation = pdata.transform.rotation;
		}
	}
	private List<SheepData> BuildSheepData(){
		List<SheepData> sheepDataList = new List<SheepData>();

		foreach(GameObject sheep in sheepOBJHolder.transform){
			SheepData sd = new SheepData ();
			sd.transform = sheep.transform;
			sd.sheepstate = sheep.GetComponent<StateCartridgeController> ().state;

			sheepDataList.Add (sd);
		}
		return sheepDataList;
	}
	private void LoadSavedSheepData(List<SheepData> sheepdatalist){
		foreach(SheepData sd in sheepdatalist){
			GameObject newSheep = Instantiate (sheepPrefab, sd.transform.position, sd.transform.rotation);
			newSheep.GetComponent<StateCartridgeController> ().state = sd.sheepstate;
		}
	}
	private List<BuildingData> BuildBuildingData(){
		List<BuildingData> buildingDataList = new List<BuildingData> ();
		foreach(GameObject building in buildingOBJHolder.transform){
			BuildingData bd = new BuildingData ();
			bd.transform = building.transform;

			buildingDataList.Add (bd);
		}
		return buildingDataList;
	}
	private void LoadSavedBuildingData(List<BuildingData> buildingdatalist){
		foreach(BuildingData bd in buildingdatalist){
			Instantiate (buildingPrefab, bd.transform.position, bd.transform.rotation);
		}
	}
}

[Serializable]
class GameData {
	public float score;
	public int numExistingSheep;
	public PlayerData playerdata;
	public List<SheepData> sheepdata;
	public List<BuildingData> buildingdata;

}
