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
		pd.positionX = transform.position.x;
		pd.positionY = transform.position.y;
		pd.positionZ = transform.position.z;

		pd.rotationW = transform.rotation.w;
		pd.rotationX = transform.rotation.x;
		pd.rotationY = transform.rotation.y;
		pd.rotationZ = transform.rotation.z;

		return pd;
	}
	public void LoadPlayerData(PlayerData pd){
		transform.position = new Vector3 (pd.positionX, pd.positionY, pd.positionZ);
		transform.rotation = new Quaternion (pd.rotationX, pd.rotationY, pd.rotationZ, pd.rotationW);
	}
}

[Serializable]
public class PlayerData {
	public float positionX;
	public float positionY;
	public float positionZ;
	public float rotationX;
	public float rotationY;
	public float rotationZ;
	public float rotationW;
}