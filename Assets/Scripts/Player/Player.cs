using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;

public class Player : MonoBehaviour {

	public float speed;
	public float jumpForce;

	InputManager inputManager;
	Rigidbody rb;

	private void Awake() {
		inputManager = GetComponent<InputManager>();
		rb = GetComponent<Rigidbody> ();
	}

	bool isGrounded;
	void OnCollisionStay() {
		isGrounded = true;
	}
	void OnCollisionEnter(Collision col){
		if(col.gameObject.CompareTag("Sheep")){
			col.gameObject.GetComponent<StateCartridgeController> ().state = StateCartridgeController.State.Dead;
		}
	}	
	void FixedUpdate () {
		// Forward/backward
		rb.MovePosition (transform.position + transform.forward * inputManager.CurrentForward * Time.deltaTime * speed);
		// Rotation
		Vector3 tempRot = transform.rotation.eulerAngles;
		tempRot.y += inputManager.CurrentRotation * Time.deltaTime;
		rb.MoveRotation (Quaternion.Euler(tempRot));
		// Jump
		if (inputManager.CurrentJump == 1 && isGrounded) {
			isGrounded = false;
			rb.AddForce (Vector3.up*jumpForce, ForceMode.Impulse);
		}
	}
	public PlayerData BuildPlayerData(){
		PlayerData pd = new PlayerData ();
		pd.saveGameObjectDetails (gameObject);

		return pd;
	}
	public void LoadPlayerData(PlayerData pd){
		transform.position = pd.getPositionVector3();
		transform.rotation = pd.getRotationQuaternion ();
	}
}

[Serializable]
public class PlayerData : TransformData {
	//leaving this open in case there ever becomes more data for player
}