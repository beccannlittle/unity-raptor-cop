using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;
using UnityEngine.UI;

public class AudioManager : MonoBehaviour {

	private AudioSource musicAudioSource;

	void Awake() {
		musicAudioSource = gameObject.GetComponent<AudioSource> ();
	}

	public void SetMusic(AudioClip newMusic) {
		musicAudioSource.clip = newMusic;
		musicAudioSource.Play ();
	}

	public void MuteMusicVolume(bool value){
		musicAudioSource.mute = value;
	}

}
