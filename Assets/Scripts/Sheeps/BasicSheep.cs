using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicSheep : MonoBehaviour
{
	public Sheep sheepType;
	public float hunger;
	public float health;
	public float hungerDecayRateAmount;
	public float hungerDecayRateTime;
	public float hungerThresholdMin;
	public float hungerThresholdMax;
	public float startledDistance;
	public float petrifiedDistance;
	public float distSheepThreshold;

	public float angleOfSight = 25.0f;
	public float rotationSpeed = 5.0f;

	void Start ()
	{
		InitializeStartingValues (sheepType);
	}

	void InitializeStartingValues (Sheep sheepT)
	{
		hunger = sheepT.getHunger ();
		health = sheepT.getHealth ();
		hungerDecayRateAmount = sheepT.getHungerDecayRateAmount ();
		hungerDecayRateTime = sheepT.getHungerDecayRateTime ();
		hungerThresholdMax = sheepT.getHungerThresholdMax ();
		hungerThresholdMin = sheepT.getHungerThresholdMin ();
		startledDistance = sheepT.getStartledDistance ();
		petrifiedDistance = sheepT.getPetrifiedDistance ();
		angleOfSight = sheepT.getAngleOfSight ();
		rotationSpeed = sheepT.getRotationSpeed ();
		distSheepThreshold = 50.0f;
	}

	public void TurnTo (Transform target)
	{
		Vector3 _direction = (target.position - transform.position).normalized;
		Quaternion _lookRotation = Quaternion.LookRotation (_direction);
		transform.rotation = Quaternion.Slerp (transform.rotation, _lookRotation, Time.deltaTime * rotationSpeed);
	}

	public void TickHunger (bool upDown)
	{
		//increment=false decrement=true
		if (upDown) {
			hunger = hunger - (hungerDecayRateAmount * Time.deltaTime);
		} else {
			hunger = hunger + (hungerDecayRateAmount * Time.deltaTime);
		}
	}

	public void TakeDamage (float damage)
	{
		health = health - (damage * Time.deltaTime);
	}

	public void Heal (float regenAmount)
	{
		health = health + (regenAmount * Time.deltaTime);
	}

	public bool CanSeeTarget (GameObject target)
	{
		Vector3 targetDirection = target.transform.position - transform.position;
		float angleToTarget = Vector3.Angle (targetDirection, transform.forward);
		if (angleToTarget <= angleOfSight) {
			return true;
		} else {
			return false;
		}
	}

	public void Die ()
	{
		Destroy (gameObject);
	}

}
