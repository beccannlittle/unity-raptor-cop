using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

using System;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;

public class GameController : MonoBehaviour {

	public static GameController control;

	void Awake () {
		if (control == null) {
			DontDestroyOnLoad (gameObject);
			control = this;
		} else if(!control.Equals(this)) {
			Destroy (gameObject);
		}
	}
	public ApplicationData GenerateApplicationData(){
		ApplicationData appData = new ApplicationData ();
		appData.audioOptions = new AudioData ();
		appData.audioOptions.masterMute = true;
		return appData;
	}
	public void LoadApplicationData(){
		Debug.Log("LoadedAppData");
	}

}

[Serializable]
public class ApplicationData {
	public AudioData audioOptions;
}

[Serializable]
public class AudioData {
	public bool masterMute;
}