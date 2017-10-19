using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager : MonoBehaviour {

	public Vector3 CurrentInput {
		get {
			return new Vector3(Input.GetAxis("Horizontal"), 0f, Input.GetAxis("Vertical"));
		}
	}

	public Vector3 CurrentMovement {
		get {
			return new Vector3(0f, 0f, Input.GetAxis ("Vertical"));
		}
	}

	public float CurrentRotation {
		get {
			return Input.GetAxis ("Horizontal");
		}
	}
}