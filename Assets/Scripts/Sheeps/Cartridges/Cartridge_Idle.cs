using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cartridge_Idle : IStateCartridge {

	// Use this for initialization
	public void Run (GameObject gobject) {
		gobject.GetComponent<SheepEnemy> ().ResetMoveSpeed ();
		gobject.GetComponent<SheepEnemy> ().Wander ();
		if (gobject.GetComponent<SheepEnemy> ().m_rage.AboveMaxThreshold ()) {
			gobject.GetComponent<StateCartridgeController> ().state = StateCartridgeController.State.Attack;
		}
		gobject.GetComponent<SheepEnemy> ().m_rage.addVal (2.0f * Time.deltaTime);
	}

}
