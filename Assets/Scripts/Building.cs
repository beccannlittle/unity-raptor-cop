using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;

public class Building : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}

}

[Serializable]
class BuildingData {
	public Transform transform;

}