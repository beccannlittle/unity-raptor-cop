using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;

public class Player : MonoBehaviour {

	public float speed;
	public float jumpForce;

	Rigidbody rb;

	private void Awake() {
		rb = GetComponent<Rigidbody> ();
	}

	bool isGrounded;
	void OnCollisionStay() {
		isGrounded = true;
	}
	void OnCollisionEnter(Collision col){
		if(col.gameObject.CompareTag("Sheep")){
			col.gameObject.GetComponent<SheepEnemy> ().Die ();
		}
	}	
	void FixedUpdate () {
		// Forward/backward
		rb.MovePosition (transform.position + transform.forward * Input.GetAxis ("Vertical") * Time.deltaTime * speed);
		// Rotation
		Vector3 tempRot = transform.rotation.eulerAngles;
		tempRot.y += Input.GetAxis ("Horizontal") * 360 * Time.deltaTime;
		rb.MoveRotation (Quaternion.Euler(tempRot));
		// Jump
		if (Input.GetAxis ("Jump") == 1 && isGrounded) {
			isGrounded = false;
			rb.AddForce (Vector3.up*jumpForce, ForceMode.Impulse);
		}
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