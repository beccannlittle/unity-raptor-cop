using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour {

	public float playerSpeed;

	InputManager inputManager;

	private void Awake() {
		inputManager = GetComponent<InputManager>();
	}

	void Update () {
		transform.Translate(inputManager.CurrentMovement * Time.deltaTime * playerSpeed);
		Vector3 tempRot = transform.rotation.eulerAngles;
		tempRot.y += inputManager.CurrentRotation * Time.deltaTime;
		transform.rotation = new Quaternion (tempRot);
	}
}