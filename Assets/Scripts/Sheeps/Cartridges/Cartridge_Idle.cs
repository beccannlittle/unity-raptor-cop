using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cartridge_Idle : IStateCartridge {

	// Use this for initialization
	public void Run (GameObject gobject) {
		gobject.GetComponent<SheepEnemy> ().Wander ();
	}

}
