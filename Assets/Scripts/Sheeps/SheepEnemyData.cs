using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using System;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;

public class SheepEnemyData : MonoBehaviour {
	public SheepEnemy sheepType;

	public float hunger;
	public float health;

	public float hungerDecayRateAmount;
	public float hungerDecayRateTime;

	public float startledDistance;

	public float petrifiedDistance;

	public float angleOfSight;

	//Wandering Vars
	public float wanderDistance;
	private NavMeshAgent navAgent;

	void Start ()
	{
		InitializeStartingValues (sheepType);
		//SaveData ();
	}

	void InitializeStartingValues (SheepEnemy sheepT)
	{
		hunger = sheepT.getStartingHunger ();
		health = sheepT.getStartingHealth ();

		hungerDecayRateAmount = sheepT.getHungerDecayRateAmount ();
		hungerDecayRateTime = sheepT.getHungerDecayRateTime ();

		startledDistance = sheepT.getStartledDistance ();
		petrifiedDistance = sheepT.getPetrifiedDistance ();

		angleOfSight = sheepT.getAngleOfSight ();
		wanderDistance = sheepT.getWanderDistance ();

		navAgent = gameObject.GetComponent<NavMeshAgent> ();
		navAgent.speed = sheepT.getMoveSpeed ();
		navAgent.angularSpeed = sheepT.getRotationSpeed ();
	}

	public void Wander(){
		if(navAgent.remainingDistance <= 5.0f){
			Vector3 newPos = RandomNavSphere (transform.position, wanderDistance, -1);
			navAgent.SetDestination (newPos);
		}
	}

	public static Vector3 RandomNavSphere(Vector3 origin, float dist, int layermask){
		Vector3 randDirection = UnityEngine.Random.insideUnitSphere * dist;
		randDirection += origin;
		NavMeshHit navHit;
		NavMesh.SamplePosition (randDirection, out navHit, dist, layermask);
		return navHit.position;
	}

	public void TurnTo (Transform target)
	{
		Vector3 _direction = (target.position - transform.position).normalized;
		Quaternion _lookRotation = Quaternion.LookRotation (_direction);
		transform.rotation = Quaternion.Slerp (transform.rotation, _lookRotation, Time.deltaTime *  navAgent.angularSpeed);
	}

	public bool CanSeeTarget (GameObject target)
	{
		Vector3 targetDirection = target.transform.position - transform.position;
		float angleToTarget = Vector3.Angle (targetDirection, transform.forward);
		if (angleToTarget <=  angleOfSight) {
			return true;
		} else {
			return false;
		}
	}
	public void TickHunger (bool upDown)
	{
		//increment=false decrement=true
		if (upDown) {
			hunger =  hunger - ( hungerDecayRateAmount * Time.deltaTime);
		} else {
			hunger =  hunger + ( hungerDecayRateAmount * Time.deltaTime);
		}
	}

	public void TakeDamage (float damage)
	{
		 health =  health - (damage * Time.deltaTime);
	}

	public void Heal (float regenAmount)
	{
		 health =  health + (regenAmount * Time.deltaTime);
	}

	public void Die ()
	{
		Destroy (gameObject);
	}
}

[Serializable]
class SheepData {
	public float positionX;
	public float positionY;
	public float positionZ;
	public float rotationX;
	public float rotationY;
	public float rotationZ;
	public StateCartridgeController.State sheepstate;

}