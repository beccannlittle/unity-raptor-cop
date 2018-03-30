using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cartridge_Attack : IStateCartridge {

	public void Run (GameObject gobject) {
		SheepEnemy s = gobject.GetComponent<SheepEnemy> ();
		s.QuickenMoveSpeed ();
		s.AttackClosestBuilding ();
		if (gobject.GetComponent<StateCartridgeController> ().state.Equals(StateCartridgeController.State.Attack)) {
			gobject.GetComponent<SheepEnemy> ().m_rage.addVal (-10.0f * (Time.deltaTime));
		}
		if(s.m_rage.BelowMinThreshold()){
			gobject.GetComponent<StateCartridgeController> ().state = StateCartridgeController.State.Idle;
		}
	}
}
