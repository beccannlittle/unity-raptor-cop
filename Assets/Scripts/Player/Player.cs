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