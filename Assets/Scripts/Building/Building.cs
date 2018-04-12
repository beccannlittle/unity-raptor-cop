using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;

public class Building : MonoBehaviour {
	public Meter m_health;

	public void Awake(){
		Initialize ();
	}

	public void Initialize(){
		m_health = new Meter (45.0f,0.0f,45.0f,5.0f,45.0f);
	}

	public void TakeDamage(float dmg){
		m_health.addVal (-dmg);
		Debug.Log ("I have "+m_health.val+" Health Left");
		if (m_health.val <= m_health.minVal) {
			this.Die ();
		}
	}
	public void Die(){
		Vector3 deadzone = new Vector3 (0, -9000f, 0);
		gameObject.SetActive (false);
		gameObject.transform.SetPositionAndRotation (deadzone, Quaternion.identity);
		GameObject.Destroy (gameObject);
	}
	public BuildingData BuildBuildingData(){
		BuildingData bd = new BuildingData ();
		//Health
		bd.health = GetComponent<Building> ().m_health.val;
		//Location
		bd.positionX = transform.position.x;
		bd.positionY = transform.position.y;
		bd.positionZ = transform.position.z;
		//Rotation
		bd.rotationW = transform.rotation.w;
		bd.rotationX = transform.rotation.x;
		bd.rotationY = transform.rotation.y;
		bd.rotationZ = transform.rotation.z;

		return bd;

	}
}

[Serializable]
public class BuildingData {
	public float health;
	public float positionX;
	public float positionY;
	public float positionZ;
	public float rotationX;
	public float rotationY;
	public float rotationZ;
	public float rotationW;
}