using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraLookAt : MonoBehaviour {

	public GameObject target;
	public float followDistance;
	public float rotAngle;
	public float downAngle;

	InputManager inputManager;

	private void Awake() {
		inputManager = target.GetComponent<InputManager>();
	}
	
	// Update is called once per frame
	void LateUpdate () {
		rotAngle = inputManager.CurrentRotation;
		Quaternion rotation = Quaternion.AngleAxis (rotAngle, Vector3.up) * Quaternion.AngleAxis (downAngle, Vector3.right);
		Vector3 direction = rotation * Vector3.forward;
		transform.position = target.transform.position + (direction.normalized*followDistance);
		transform.LookAt (target.transform);
	}
}
