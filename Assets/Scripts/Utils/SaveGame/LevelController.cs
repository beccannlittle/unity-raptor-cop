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

	//PauseScene,ResumeScene
}

[Serializable]
public abstract class LevelData {
	//Generic Level Data not including the scene specific things.
	public string sceneName;
	public float score;
}