using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using System;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;

[Serializable]
public class TransformData {

	public float positionX;
	public float positionY;
	public float positionZ;

	public float rotationX;
	public float rotationY;
	public float rotationZ;
	public float rotationW;

	public Vector3 getPositionVector3(){
		return new Vector3 (positionX, positionY, positionZ);
	}

	public Quaternion getRotationQuaternion(){
		return new Quaternion (rotationX, rotationY, rotationZ, rotationW);
	}

	public void saveGameObjectDetails(GameObject gobj){
		savePosition (gobj);
		saveRotation (gobj);
	}

	public void savePosition(GameObject gobj){
		positionX = gobj.transform.position.x;
		positionY = gobj.transform.position.y;
		positionZ = gobj.transform.position.z;
	}
	public void saveRotation(GameObject gobj) {
		rotationW = gobj.transform.rotation.w;
		rotationX = gobj.transform.rotation.x;
		rotationY = gobj.transform.rotation.y;
		rotationZ = gobj.transform.rotation.z;
	}

}
