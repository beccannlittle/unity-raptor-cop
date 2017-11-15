using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraLookAt : MonoBehaviour {

	public GameObject target;
	public float followDistance;
	public float downAngle;
	public float playerRotation;

	InputManager inputManager;

	private void Awake() {
		inputManager = target.GetComponent<InputManager>();
	}

	// Update is called once per frame
	void LateUpdate () {
		// insteda of changing the rotation I need to change the position. lookat will take 
		// care of rotation for me.
		Vector3 tempRot = transform.rotation.eulerAngles;
		tempRot.y += inputManager.CurrentRotation * Time.deltaTime;
		Debug.Log (tempRot);
//		Quaternion rotation = Quaternion.Euler(tempRot) * Quaternion.AngleAxis (downAngle, Vector3.right);
		transform.rotation = Quaternion.Euler(tempRot);
//		transform.rotation = rotation;
//		Vector3 direction = rotation * Vector3.forward;
//		transform.position = target.transform.position + (direction.normalized*followDistance);
		transform.LookAt (target.transform);
	}
}
