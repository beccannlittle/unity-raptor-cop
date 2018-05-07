using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ScoreManager : MonoBehaviour {

	private ConstantsManager constantsManager;
	private SaveLoadGame saveManager;

	private float playerScore;
	private int numSheepInExistence;
	private int numBuildingsInExistence;

	void Awake () {
		GameObject gameController = GameObject.FindGameObjectWithTag ("GameController");
		constantsManager = gameController.GetComponent<ConstantsManager> ();
		saveManager = gameController.GetComponent<SaveLoadGame> ();
		QuerySheepRemaining ();
		QueryBuildingsRemaining ();
	}
		
	// Getters and setters 
	public float getPlayerScore() { return playerScore; }
	public void setPlayerScore(float newScore) { playerScore = newScore; }
	public int getNumSheepInExistence() { return numSheepInExistence; }
	public void setNumSheepInExistence(int newNum) { numSheepInExistence = newNum; }

	public void AddScore(float addvalue) {
		playerScore += addvalue;
	}

	public void AddSheep(int addvalue){
		numSheepInExistence += addvalue;
		if (numSheepInExistence <= 0) {
			EndGame (true);
		}
	}

	public void AddBuilding(int addvalue){
		numBuildingsInExistence += addvalue;
		if (numBuildingsInExistence <= 0) {
			EndGame (false);
		}
	}

	private void EndGame(bool hasWon){
		if (hasWon) {
			Debug.Log ("You won the game!");
			SceneManager.LoadScene (constantsManager.getCredits ());
			if (saveManager != null) {
				saveManager.ClearSaveData ();
			}
		} else {
			SceneManager.LoadScene (constantsManager.getLoseScreen ());
			if (saveManager != null) {
				saveManager.ClearSaveData ();
			}
		}
	}

	public void QuerySheepRemaining(){
		GameObject[] listOfSheepRemaining = GameObject.FindGameObjectsWithTag ("Sheep");
		if (listOfSheepRemaining != null) {
			numSheepInExistence = listOfSheepRemaining.Length;
		}
	}

	public void QueryBuildingsRemaining(){
		GameObject[] listOfBuildingsRemaining = GameObject.FindGameObjectsWithTag ("Building");
		if (listOfBuildingsRemaining == null) {
			numBuildingsInExistence = listOfBuildingsRemaining.Length;
		}
	}

}
