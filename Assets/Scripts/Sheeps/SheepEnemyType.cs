using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu (fileName = "New Sheep", menuName = "Sheep Enemy Type")]
public class SheepEnemyType : ScriptableObject
{
	public float healthStarting = 15.0f;
	public float healthMax = 15.0f;

	public float hungerStarting = 50.0f;
	public float hungerMin = 30.0f;
	public float hungerMax = 100.0f;
	public float hungerDecayAmount = 10.0f;
	public float hungerDecayInterval = 3.0f;

	public float startledDistance = 50.0f;

	public float petrifiedDistance = 30.0f;

	public float moveSpeed = 10.0f;
	public float wanderDistance = 35.0f;
	public float rotationSpeed = 100.0f;
	public float angleOfSight = 25.0f;

	public float getStartingHealth (){return healthStarting;}

	public float getMaxHealth (){return healthMax;}

	public float getStartingHunger (){return hungerStarting;}

	public float getHungerDecayRateAmount (){return hungerDecayAmount;}

	public float getHungerDecayRateTime (){return hungerDecayInterval;}

	public float getMinHunger (){return hungerMin;}

	public float getMaxHunger (){return hungerMax;}

	public float getStartledDistance (){return startledDistance;}

	public float getPetrifiedDistance (){return petrifiedDistance;}

	public float getMoveSpeed (){return moveSpeed;}

	public float getWanderDistance (){return wanderDistance;}

	public float getRotationSpeed (){return rotationSpeed;}

	public float getAngleOfSight (){return angleOfSight;}

}
