using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;

public class Player : MonoBehaviour {

	public float speed;
	public float attackForce;
	public float attackCooldown;

	Rigidbody rb;
	Collider attackCol;

	private void Awake() {
		rb = GetComponent<Rigidbody> ();
		attackCol = GetComponentInChildren<SphereCollider> ();
		attackCol.enabled = false;
	}

	void OnCollisionEnter(Collision col){
		Collider myCollider = col.contacts [0].thisCollider;
		if (myCollider == attackCol) {
			if (col.gameObject.CompareTag("Sheep")){
				col.gameObject.GetComponent<SheepEnemy> ().Die ();
			}
		}
	}	

	void FixedUpdate () {
		Move ();
		if (Input.GetAxis ("Attack") == 1 && !isAttacking) {
			StartCoroutine (Attack ());
		}
	}

	private void Move() {
		// Forward/backward
		rb.MovePosition (transform.position + transform.forward * Input.GetAxis ("Vertical") * Time.deltaTime * speed);
		// Rotation
		Vector3 tempRot = transform.rotation.eulerAngles;
		tempRot.y += Input.GetAxis ("Horizontal") * 360 * Time.deltaTime;
		rb.MoveRotation (Quaternion.Euler(tempRot));
	}

	private bool isAttacking;

	IEnumerator Attack() {
		isAttacking = true;
		attackCol.enabled = true;
		rb.MovePosition (transform.position + transform.forward * Time.deltaTime * attackForce);
		yield return new WaitForSeconds (attackCooldown);
		attackCol.enabled = false;
		isAttacking = false;
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