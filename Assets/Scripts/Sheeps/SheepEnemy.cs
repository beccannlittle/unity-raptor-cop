using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using System;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;

public class SheepEnemy : MonoBehaviour {
	public SheepEnemyType sheepType;

	public Meter m_rage;
	public float attackDistance;
	private GameObject attackTarget;
	private Vector3 moveTowards;

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

	void Awake ()
	{
		InitializeStartingValues (sheepType);
	}

	void InitializeStartingValues (SheepEnemyType sheepT)
	{
		hunger = sheepT.getStartingHunger ();
		health = sheepT.getStartingHealth ();

		m_rage = new Meter (UnityEngine.Random.Range (0.0f, 40.0f), 0.0f, 100.0f, 30.0f, 70.0f);
		attackDistance = sheepT.getAttackDistance ();

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

	public void AttackClosestBuilding(){
		float swingDistance = 10.0f;
		float sheepDmg = 20.0f;
		if (attackTarget == null) {
			attackTarget = FindClosestBuilding ();
		}
		if (attackTarget != null) {
			RaycastHit hit;
			//Debug.DrawRay (transform.position, (attackTarget.transform.position - transform.position).normalized * swingDistance, Color.green);
			//Debug.Log ("Did" + gameObject.name + " hit:" + (Physics.Raycast (transform.position, (attackTarget.transform.position - transform.position).normalized, out hit, swingDistance)));
			if (Physics.Raycast (transform.position, (attackTarget.transform.position - transform.position).normalized, out hit, swingDistance)) {
				//Debug.Log ("attack Target" + attackTarget.name + "!");
				attackTarget.GetComponent<Building> ().TakeDamage(sheepDmg);
				navAgent.SetDestination (transform.position);
				attackTarget = null;
				m_rage.SetToMin ();
			} else {
				navAgent.SetDestination (attackTarget.transform.position);
			}
		} else {
			FleeTheScene ();
		}
	}

	private GameObject FindClosestBuilding() {
		GameObject buildingOBJHolder = GameObject.FindGameObjectWithTag ("BuildingList");
		GameObject closestBuilding = null;
		float closestDist = this.attackDistance;
		if (buildingOBJHolder != null) {
			RaycastHit hit;
			foreach (Transform b in buildingOBJHolder.transform) {
				if (Physics.Raycast (transform.position, (b.position - transform.position).normalized, out hit, attackDistance)) {
					if (hit.distance < closestDist) {
						closestBuilding = b.gameObject;
						closestDist = hit.distance;
					}
				}
			}
		}
		return closestBuilding;
	}

	public void FleeTheScene () {
		if (navAgent.isStopped) {
			navAgent.isStopped = false;
		}
		if (navAgent.remainingDistance <= 5.0f) {
			Vector3 newPos = RandomNavSphere (transform.position, wanderDistance*3, -1);
			navAgent.SetDestination (newPos);
		}
	}

	public void Wander() {
		if(navAgent.remainingDistance <= 5.0f) {
			Vector3 newPos = RandomNavSphere (transform.position, wanderDistance, -1);
			navAgent.SetDestination (newPos);
		}
	}

	public static Vector3 RandomNavSphere(Vector3 origin, float dist, int layermask) {
		Vector3 randDirection = UnityEngine.Random.insideUnitSphere * dist;
		randDirection += origin;
		NavMeshHit navHit;
		NavMesh.SamplePosition (randDirection, out navHit, dist, layermask);
		return navHit.position;
	}

	public void TurnTo (Transform target) {
		//March 30 - currently causes the sheep to move backwards and shouldn't do that.
		Vector3 _direction = (target.position - transform.position).normalized;
		Quaternion _lookRotation = Quaternion.LookRotation (_direction);
		transform.rotation = Quaternion.Slerp (transform.rotation, _lookRotation, Time.deltaTime * navAgent.angularSpeed);
	}

	public bool CanSeeTarget (GameObject target) {
		//March 26 - Currently not used but should be used for Startled state
		Vector3 targetDirection = target.transform.position - transform.position;
		float angleToTarget = Vector3.Angle (targetDirection, transform.forward);
		if (angleToTarget <= angleOfSight) {
			return true;
		} else {
			return false;
		}
	}
	public void TickHunger (bool upDown) {
		//increment=false decrement=true
		if (upDown) {
			hunger =  hunger - ( hungerDecayRateAmount * Time.deltaTime);
		} else {
			hunger =  hunger + ( hungerDecayRateAmount * Time.deltaTime);
		}
	}

	public void TakeDamage (float damage) {
		float newhealth = health - (damage * Time.deltaTime);
		if (newhealth < 0.0f) {
			health = 0.0f;
		} else {
			health = newhealth;
		}
	}

	public void Heal (float regenAmount) {
		float newhealth = health + (regenAmount * Time.deltaTime);
		if (newhealth > sheepType.getMaxHealth ()) {
			health = sheepType.getMaxHealth ();
		} else {
			health = newhealth;
		}
	}

	public void ResetMoveSpeed(){
		navAgent.speed = sheepType.getMoveSpeed ();
	}

	public void QuickenMoveSpeed(){
		if (navAgent.speed.Equals (sheepType.getMoveSpeed ())) {
			navAgent.speed = sheepType.getMoveSpeed () * 2;
		}
	}

	public void Die () {
		Destroy (gameObject);
	}
	public SheepData BuildSheepData(){
		SheepData sd = new SheepData ();
		sd.positionX = transform.position.x;
		sd.positionY = transform.position.y;
		sd.positionZ = transform.position.z;

		sd.rotationW = transform.rotation.w;
		sd.rotationX = transform.rotation.x;
		sd.rotationY = transform.rotation.y;
		sd.rotationZ = transform.rotation.z;
		sd.sheepstate = gameObject.GetComponent<StateCartridgeController> ().state;
		return sd;
	}
}

[Serializable]
public class SheepData {
	public float positionX;
	public float positionY;
	public float positionZ;
	public float rotationX;
	public float rotationY;
	public float rotationZ;
	public float rotationW;
	public StateCartridgeController.State sheepstate;

}