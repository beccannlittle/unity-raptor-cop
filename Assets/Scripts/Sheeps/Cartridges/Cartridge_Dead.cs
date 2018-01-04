using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cartridge_Dead : IStateCartridge {
	private Vector3 deadzone = new Vector3 (0, -9000f, 0);
	// Use this for initialization
	public void Run (GameObject gobject) {
		gobject.SetActive (false);
		gobject.transform.SetPositionAndRotation (deadzone, Quaternion.identity);
		GameObject.Destroy (gobject);
	}

}
