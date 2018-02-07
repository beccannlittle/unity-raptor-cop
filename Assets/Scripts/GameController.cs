using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour {

	private static GameController controller;

	void Awake () {
		if (controller == null) {
			DontDestroyOnLoad (gameObject);
			controller = this;
		} else if(!controller.Equals(this)) {
			Destroy (gameObject);
		}
	}
}
