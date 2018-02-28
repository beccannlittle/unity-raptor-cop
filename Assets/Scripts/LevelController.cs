using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelController : MonoBehaviour {

	public AudioClip mainMusic;

	private GameObject gameController;

	void Awake() {
		gameController = GameObject.FindGameObjectWithTag ("GameController");
	}

	void Start () {
		gameController.GetComponent<AudioManager> ().SetMusic (mainMusic);
	}

}
