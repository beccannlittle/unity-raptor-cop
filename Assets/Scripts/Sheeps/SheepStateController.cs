using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SheepStateController : MonoBehaviour
{
	public enum State
	{
		Idle,
		Graze,
		Startled,
		Petrified,
		Dead
	}

	public State state;
	public GameObject[] friends;
	private BasicSheep baseSheep;
	private GameObject player;

	void Start ()
	{
		state = State.Idle;
		baseSheep = gameObject.GetComponent<BasicSheep> ();
		player = GameObject.FindWithTag ("Player");
	}

	public void Update ()
	{
		//Do whatever the current State tells you to do
		if (state.Equals (State.Idle)) {
			DoIdleThings ();
		} else if (state.Equals (State.Graze)) {
			DoGrazeThings ();
		} else if (state.Equals (State.Startled)) {
			DoStartledThings ();
		} else if (state.Equals (State.Petrified)) {
			DoPetrifiedThings ();
		} else if (state.Equals (State.Dead)) {
			DoDeadThings ();
		}
		//Should I change my State?
		CheckChangeState ();
	}

	void CheckChangeState ()
	{
		if (baseSheep.health <= 0) {
			state = State.Dead;
		} else if (DistanceToPlayer () < baseSheep.petrifiedDistance && baseSheep.CanSeeTarget(player)) {
			state = State.Petrified;
		} else if (state != State.Petrified && DistanceToPlayer () < baseSheep.startledDistance && baseSheep.CanSeeTarget(player)) {
			state = State.Startled;
		} else if (baseSheep.hunger <= 0) {
			state = State.Petrified;
		} else if (baseSheep.hunger <= baseSheep.hungerThresholdMin) {
			state = State.Graze;
		} else if (baseSheep.hunger >= baseSheep.hungerThresholdMax) {
			state = State.Idle;
		}
	}

	void DoIdleThings ()
	{
		ChangeColor (Color.white);
		//Choose One Wander or Nuzzle based on if a sheep is nearby and a random chance.
		GameObject nearbySheep = NearbySheepExists ();
		if ((baseSheep.affectionMeter <= 30 && !(baseSheep.affectionMeter >= 100)) && nearbySheep != null) {
			baseSheep.NuzzleNearbySheep (nearbySheep);	
		} else {
			baseSheep.Wander ();
		}
			
		baseSheep.TickHunger (true);
	}

	void DoGrazeThings ()
	{
		ChangeColor (Color.green);
		baseSheep.TickHunger (false);
	}

	void DoStartledThings ()
	{
		ChangeColor (Color.yellow);
		baseSheep.TickHunger (false);
		baseSheep.MoveAwayFrom (player);
		baseSheep.TurnTo (player.transform);
	}

	void DoPetrifiedThings ()
	{
		ChangeColor (Color.grey);
		baseSheep.TakeDamage (2.0f);
	}

	void DoDeadThings ()
	{
		ChangeColor (Color.black);
		baseSheep.Die ();
	}

	public float DistanceToPlayer ()
	{
		return Mathf.Abs (Vector3.Distance (player.transform.position, gameObject.transform.position));
	}
	public bool NearbySheepExists(){
		GameObject closestSheepFriend = GetClosestFriend ("sheep");
		float distanceToClosestSheep = (closestSheepFriend.transform.position - gameObject.transform.position).magnitude;
		if(distanceToClosestSheep <= baseSheep.distSheepThreshold){
			return true;
		}

		return false;
	}
	public GameObject GetClosestFriend(string friendType){
		float closestDistanceSqr = Mathf.Infinity;
		GameObject closestFriend = null;
		foreach (GameObject friend in friends){
			float distanceSqrToFriend = (friend.transform.position - gameObject.transform.position).sqrMagnitude;
			if (distanceSqrToFriend < closestDistanceSqr) {
				closestDistanceSqr = distanceSqrToFriend;
				closestFriend = friend;
			}
		}
		return closestFriend;
	}
	public void ChangeColor (Color newcolor)
	{
		foreach (Renderer childRenderer in GetComponentsInChildren<Renderer>()) {
			childRenderer.material.color = newcolor;
		}
	}
}
