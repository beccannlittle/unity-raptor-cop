using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicSheep : MonoBehaviour {
	public Sheep sheepType;
	public float hunger;
	public float health;
	public float hungerDecayRateAmount;
	public float hungerDecayRateTime;
	public float hungerThresholdMin;
	public float hungerThresholdMax;

	void Start(){
		InitializeStartingValues(sheepType);
		Debug.Log ("Starting Hunger Value: "+hunger);
	}

	void InitializeStartingValues(Sheep sheepT) {
		hunger = sheepT.getHunger ();
		health = sheepT.getHealth ();
		hungerDecayRateAmount = sheepT.getHungerDecayRateAmount ();
		hungerDecayRateTime = sheepT.getHungerDecayRateTime ();
		hungerThresholdMax = sheepT.getHungerThresholdMax ();
		hungerThresholdMin = sheepT.getHungerThresholdMin ();
	}

}
