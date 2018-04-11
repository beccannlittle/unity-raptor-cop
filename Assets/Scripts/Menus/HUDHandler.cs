using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HUDHandler : MonoBehaviour {

	public Text scoreboardText;
	public Text sheepExistText;

	private ScoreManager scoreManager;

	void Awake() {
		GameObject levelController = GameObject.FindGameObjectWithTag ("LevelController");
		scoreManager = levelController.GetComponent<ScoreManager> ();
	}

	void Update(){
		UpdateScoreUI ();
		UpdateSheepExistUI ();
	}

	public void UpdateScoreUI(){
		if (scoreboardText != null) {
			scoreboardText.text = "Score: " + scoreManager.getPlayerScore();
		}
	}

	public void UpdateSheepExistUI(){
		if (sheepExistText != null) {
			sheepExistText.text = "Sheep remaining: " + scoreManager.getNumSheepInExistence ();
		}
	}

}
