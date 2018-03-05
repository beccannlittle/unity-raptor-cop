using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ScoreManager : MonoBehaviour {

	public int initialSheepNum;

	private ConstantsManager constantsManager;
	private SaveManager saveManager;

	private float playerScore;
	private int numSheepInExistence;

	void Awake () {
		GameObject gameController = GameObject.FindGameObjectWithTag ("GameController");
		constantsManager = gameController.GetComponent<ConstantsManager> ();
		saveManager = gameController.GetComponent<SaveManager> ();

		constantsManager.setupScene ();
		if (numSheepInExistence == 0) numSheepInExistence = initialSheepNum;
	}
		
	// Getters and setters 
	public float getPlayerScore() { return playerScore; }
	public void setPlayerScore(float newScore) { playerScore = newScore; }
	public int getNumSheepInExistence() { return numSheepInExistence; }
	public void setNumSheepInExistence(int newNum) { numSheepInExistence = newNum; }

	public void AddScore(float addvalue){
		playerScore += addvalue;
	}

	public void AddSheep(int addvalue){
		numSheepInExistence += addvalue;
		if (numSheepInExistence <= 0) {
			EndGame ();
		}
	}

	private void EndGame(){
		Debug.Log ("You won the game!");
		SceneManager.LoadScene (constantsManager.getCredits());
		saveManager.ClearSaveData ();
	}

	// I don't think we need this...
//	public void QuerySheepRemaining(){
//		GameObject[] listOfSheepRemaining = GameObject.FindGameObjectsWithTag ("Sheep");
//		numSheepInExistence = listOfSheepRemaining.Length;
//	}

}
