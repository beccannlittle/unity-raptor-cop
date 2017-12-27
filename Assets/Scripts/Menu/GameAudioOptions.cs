using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;
using UnityEngine.UI;

public class GameAudioOptions : MonoBehaviour {

	public static GameAudioOptions control;
	[Header("Audio Option Data")]
	public bool masterVolumeEnabled;
	public bool sfxVolumeEnabled;
	public bool uiVolumeEnabled;
	public bool ambientVolumeEnabled;
	public float masterVolume;
	public float sfxVolume;
	public float uiVolume;
	public float ambientVolume;

	// Use this for initialization
	void Awake () {
		if (control == null) {
			DontDestroyOnLoad (gameObject);
			control = this;
		} else if(!control.Equals(this)) {
			Destroy (gameObject);
		}
	}
	
	// Update is called once per frame
	void OnGUI () {
		GUI.Label (new Rect (10, 10, 100, 30), "Health: " + masterVolumeEnabled);
	}

	public void SaveAudioOptions(){
		BinaryFormatter bf = new BinaryFormatter ();
		FileStream file = File.Create (Application.persistentDataPath + "/gameAudioOptions.dat");

		GameAudioOptionsData optionsData = new GameAudioOptionsData ();
		optionsData.masterVolumeEnabled = masterVolumeEnabled;
		optionsData.sfxVolumeEnabled = sfxVolumeEnabled;
		optionsData.uiVolumeEnabled = uiVolumeEnabled;
		optionsData.ambientVolumeEnabled = ambientVolumeEnabled;
		optionsData.masterVolume = masterVolume;
		optionsData.sfxVolume = sfxVolume;
		optionsData.uiVolume = uiVolume;
		optionsData.ambientVolume = ambientVolume;

		bf.Serialize (file, optionsData);
		file.Close ();
	}

	public void LoadAudioOptions(){
		if(File.Exists(Application.persistentDataPath + "/gameAudioOptions.dat")){
			BinaryFormatter bf = new BinaryFormatter ();
			FileStream file = File.Open (Application.persistentDataPath + "/gameAudioOptions.dat", FileMode.Open);			
			GameAudioOptionsData data = (GameAudioOptionsData)bf.Deserialize (file);
			file.Close ();

			masterVolumeEnabled = data.masterVolumeEnabled;
			sfxVolumeEnabled = data.sfxVolumeEnabled;
			uiVolumeEnabled = data.uiVolumeEnabled;
			ambientVolumeEnabled = data.ambientVolumeEnabled;
			masterVolume = data.masterVolume;
			sfxVolume = data.sfxVolume;
			uiVolume = data.uiVolume;
			ambientVolume = data.ambientVolume;
		}
	}

	public void MuteMasterVolume(bool value){masterVolumeEnabled = value;}
	public void MuteSFXVolume(bool value){sfxVolumeEnabled = value;}
	public void MuteUIVolume(bool value){uiVolumeEnabled = value;}
	public void MuteAmbientVolume(bool value){ambientVolumeEnabled = value;}
	public void UpdateMasterVolume(Slider slider){masterVolume = slider.value;}
	public void UpdateSFXVolume(Slider slider){sfxVolume = slider.value;}
	public void UpdateUIVolume(Slider slider){uiVolume = slider.value;}
	public void UpdateAmbientVolume(Slider slider){ambientVolume = slider.value;}

}



[Serializable]
class GameAudioOptionsData {

	[Header("Audio Option Data")]
	public bool masterVolumeEnabled;
	public bool sfxVolumeEnabled;
	public bool uiVolumeEnabled;
	public bool ambientVolumeEnabled;
	public float masterVolume;
	public float sfxVolume;
	public float uiVolume;
	public float ambientVolume;
}
