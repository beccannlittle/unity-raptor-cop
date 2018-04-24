using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;

public class Player : MonoBehaviour {

	public float speed;
	public float attackForce;
	public float attackTime;

	Rigidbody rb;
	Collider attackCol;

	private void Awake() {
		rb = GetComponent<Rigidbody> ();
		attackCol = GetComponentInChildren<SphereCollider> ();
		attackCol.enabled = false;
	}

	void OnCollisionEnter(Collision col){
		Collider myCollider = col.contacts [0].thisCollider;
		Debug.Log (myCollider);
		if (myCollider == attackCol) {
			if (col.gameObject.CompareTag("Sheep")){
				col.gameObject.GetComponent<SheepEnemy> ().Die ();
			}
		}
	}	

	void FixedUpdate () {
		Move ();
		Attack();
	}

	private void Move() {
		// Forward/backward
		rb.MovePosition (transform.position + transform.forward * Input.GetAxis ("Vertical") * Time.deltaTime * speed);
		// Rotation
		Vector3 tempRot = transform.rotation.eulerAngles;
		tempRot.y += Input.GetAxis ("Horizontal") * 360 * Time.deltaTime;
		rb.MoveRotation (Quaternion.Euler(tempRot));
	}

	private void Attack() {
		if (Input.GetAxis ("Attack") == 1) {
			StartCoroutine (Fuck ());
		}
	}

	IEnumerator Fuck() {
		attackCol.enabled = true;
		rb.MovePosition (transform.position + transform.forward * Time.deltaTime * attackForce);
		yield return new WaitForSeconds (attackTime);
		attackCol.enabled = false;
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