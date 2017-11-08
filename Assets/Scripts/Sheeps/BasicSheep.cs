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

	public float angleOfSight = 25.0f;
	public float RotationSpeed = 5.0f;

	private GameObject player;
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

		player = GameObject.FindWithTag ("Player");
	}

	public void TurnTo (Transform target)
	{
		Vector3 _direction = (target.position - transform.position).normalized;
		Quaternion _lookRotation = Quaternion.LookRotation (_direction);
		transform.rotation = Quaternion.Slerp (transform.rotation, _lookRotation, Time.deltaTime * RotationSpeed);
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

	public bool CanSeePlayer ()
	{
		Vector3 targetDirection = player.transform.position - transform.position;
		float angleToPlayer = Vector3.Angle (targetDirection, transform.forward);
		if (angleToPlayer <= angleOfSight) {
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
