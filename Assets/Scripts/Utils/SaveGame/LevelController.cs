using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;
using UnityEngine.SceneManagement;


public abstract class LevelController : MonoBehaviour,LevelControllerInterface {
	public GameObject sheepPrefab;
	public GameObject buildingPrefab;
	public GameObject playerOBJ;
	public GameObject sheepOBJHolder;
	public GameObject buildingOBJHolder;

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
public interface LevelControllerInterface {
	LevelData GenerateLevelData ();
	void LoadLevelData (LevelData ld);
	//PauseScene,ResumeScene
}

[Serializable]
public abstract class LevelData {
	//Generic Level Data not including the scene specific things.
	public string sceneName;
	public float score;
}