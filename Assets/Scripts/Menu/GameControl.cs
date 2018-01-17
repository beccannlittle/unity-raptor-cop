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
		SceneManager.LoadScene (GameConstants.Scene_Credits);
		ClearSaveData ();
		Destroy (this.gameObject);
	}
	public void SaveGameData(){
		BinaryFormatter bf = new BinaryFormatter ();
		FileStream file = File.Create (GameConstants.Resource_SaveGamePath);

		SaveGameControlData (bf, file);

		file.Close ();
	}
	public void LoadGameData(){
		if(File.Exists(GameConstants.Resource_SaveGamePath)){
			ClearSceneData ();
			BinaryFormatter bf = new BinaryFormatter ();
			FileStream file = File.Open (GameConstants.Resource_SaveGamePath, FileMode.Open);			

			GameData data = (GameData)bf.Deserialize (file);
			LoadGameControlData (data);
			file.Close ();
		}
	}
	private void ClearSceneData(){
		foreach (Transform sheep in sheepOBJHolder.transform) {
			Destroy (sheep.gameObject);
		}
		foreach (Transform building in buildingOBJHolder.transform) {
			Destroy (building.gameObject);
		}
	}
	public void ClearSaveData(){
		if(File.Exists(GameConstants.Resource_SaveGamePath)){
			File.Delete (GameConstants.Resource_SaveGamePath);
		}
	}
	public void AddScore(float addvalue){
		playerscore += addvalue;
		UpdateScoreUI ();
	}
	public void UpdateScoreUI(){
		if (scoreboardText != null) {
			scoreboardText.text = GameConstants.Resource_ScorePrefix + playerscore;
		}
	}
	public void AddSheep(int addvalue){
		numSheepInExistence += addvalue;
		UpdateSheepExistUI ();
	}
	public void UpdateSheepExistUI(){
		sheepExistText.text = GameConstants.Resource_SheepExistPrefix + numSheepInExistence;
	}
	public void QuerySheepRemaining(){
		GameObject[] listOfSheepRemaining = GameObject.FindGameObjectsWithTag (GameConstants.Tag_Sheep);
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
	private void LoadGameControlData(GameData gdata){

		LoadSavedPlayer (gdata.playerdata);
		LoadSavedSheepData (gdata.sheepdata);
		LoadSavedBuildingData (gdata.buildingdata);

		playerscore = gdata.score;
		numSheepInExistence = gdata.numExistingSheep;
		Debug.Log (GameConstants.Resource_ScorePrefix + playerscore);
		Debug.Log (GameConstants.Resource_SheepExistPrefix + numSheepInExistence);
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
	private void LoadSavedPlayer(PlayerData pd){
		if(pd != null) {
			playerOBJ.transform.position =  new Vector3(pd.positionX,pd.positionY,pd.positionZ);
			playerOBJ.transform.rotation = Quaternion.Euler(pd.rotationX,pd.rotationY,pd.rotationZ);
		}
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
	private void LoadSavedSheepData(List<SheepData> sheepdatalist){
		foreach(SheepData sd in sheepdatalist){
			GameObject newSheep = Instantiate (sheepPrefab, new Vector3(sd.positionX,sd.positionY,sd.positionZ), Quaternion.Euler(sd.rotationX,sd.rotationY,sd.rotationZ), sheepOBJHolder.transform);
			newSheep.GetComponent<StateCartridgeController> ().state = sd.sheepstate;
		}
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
	private void LoadSavedBuildingData(List<BuildingData> buildingdatalist){
		foreach(BuildingData bd in buildingdatalist){
			Instantiate (buildingPrefab, new Vector3(bd.positionX,bd.positionY,bd.positionZ), Quaternion.Euler(bd.rotationX,bd.rotationY,bd.rotationZ), buildingOBJHolder.transform);
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
