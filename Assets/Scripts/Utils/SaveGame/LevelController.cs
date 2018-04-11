using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using System;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;


public interface LevelController {
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