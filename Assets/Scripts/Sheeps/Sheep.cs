using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Sheep", menuName="Enemies/Sheep")]
public class Sheep : ScriptableObject {
	public float hunger = 50.0f;
	public float health = 15.0f;
	public float hungerDecayRateAmount = 10.0f;
	public float hungerDecayRateTime = 3.0f;
	public float hungerThresholdMin = 30.0f;
	public float hungerThresholdMax = 100.0f;
	public float startledDistance = 30.0f;
	public float getHunger(){
		return hunger;
	}
	public float getHealth(){
		return health;
	}
	public float getHungerDecayRateAmount(){
		return hungerDecayRateAmount;
	}
	public float getHungerDecayRateTime(){
		return hungerDecayRateTime;
	}
	public float getHungerThresholdMin(){
		return hungerThresholdMin;
	}
	public float getHungerThresholdMax(){
		return hungerThresholdMax;
	}

	public float getStartledDistance(){
		return startledDistance;
	}
}
