using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;
using UnityEngine.UI;

public class AudioManager : MonoBehaviour {

	public AudioClip mainMusic;

	private AudioSource musicAudioSource;

	void Awake() {
		musicAudioSource = gameObject.GetComponent<AudioSource> ();
		musicAudioSource.clip = mainMusic;
		musicAudioSource.Play ();
	}

	public void MuteMusicVolume(bool value){
		musicAudioSource.mute = value;
	}

}
