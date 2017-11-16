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
	private SheepEnemyData sheepMech;
	private SheepEnemyData sheepData;
	private GameObject player;

	void Start ()
	{
		state = State.Idle;
		sheepMech = gameObject.GetComponent<SheepEnemyData> ();
		sheepData = gameObject.GetComponent<SheepEnemyData> ();
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
		if (sheepData.health <= 0) {
			state = State.Dead;
		} else if (DistanceToPlayer () < sheepData.petrifiedDistance && sheepMech.CanSeeTarget(player)) {
			state = State.Petrified;
		} else if (state != State.Petrified && DistanceToPlayer () < sheepData.startledDistance && sheepMech.CanSeeTarget(player)) {
			state = State.Startled;
		} else if (sheepData.hunger <= 0) {
			state = State.Petrified;
		} else if (sheepData.hunger <= sheepData.sheepType.getMinHunger()) {
			state = State.Graze;
		} else if (sheepData.hunger >= sheepData.sheepType.getMaxHunger()) {
			state = State.Idle;
		}
	}

	void DoIdleThings ()
	{
		ChangeColor (Color.white);
		//Choose One Wander or Nuzzle based on if a sheep is nearby and a random chance.
		GameObject nearbySheep = NearbySheepExists ();
		if (nearbySheep != null) {
			//sheepMech.NuzzleNearbySheep (nearbySheep);	
		} else {
			sheepMech.Wander ();
		}
			
		sheepMech.TickHunger (true);
	}

	void DoGrazeThings ()
	{
		ChangeColor (Color.green);
		sheepMech.TickHunger (false);
	}

	void DoStartledThings ()
	{
		ChangeColor (Color.yellow);
		sheepMech.TickHunger (false);
		//sheepMech.MoveAwayFrom (player);
		sheepMech.TurnTo (player.transform);
	}

	void DoPetrifiedThings ()
	{
		ChangeColor (Color.grey);
		sheepMech.TakeDamage (2.0f);
	}

	void DoDeadThings ()
	{
		ChangeColor (Color.black);
		sheepMech.Die ();
	}

	public float DistanceToPlayer ()
	{
		return Mathf.Abs (Vector3.Distance (player.transform.position, gameObject.transform.position));
	}
	public GameObject NearbySheepExists(){
		GameObject closestSheepFriend = GetClosestFriend ("sheep");
		float distanceToClosestSheep = (closestSheepFriend.transform.position - gameObject.transform.position).magnitude;
		if(distanceToClosestSheep <= 100.0f){
			return closestSheepFriend;
		}

		return null;
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
