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

	/*
	public void OnTriggerStay(){
		if(state != State.Dead && state != State.Petrified){
			state = State.Startled;
		}
	}

	public void OnTriggerExit(){
		if(state != State.Dead && state != State.Petrified){
			state = State.Idle;
		}
	}
	*/

	void CheckChangeState ()
	{
		if (baseSheep.health <= 0) {
			state = State.Dead;
		} else if (baseSheep.hunger <= 0) {
			state = State.Petrified;
		} else if (baseSheep.hunger <= baseSheep.hungerThresholdMin) {
			state = State.Graze;
		} else if (baseSheep.hunger >= baseSheep.hungerThresholdMax) {
			state = State.Idle;
		} else if (DistanceToPlayer () < baseSheep.petrifiedDistance) {
			state = State.Startled;
		} else if (DistanceToPlayer () < baseSheep.startledDistance) {
			state = State.Startled;
		}
	}

	void DoIdleThings ()
	{
		ChangeColor (Color.white);
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
		baseSheep.TurnTo (player);
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

	public void ChangeColor (Color newcolor)
	{
		foreach (Renderer childRenderer in GetComponentsInChildren<Renderer>()) {
			childRenderer.material.color = newcolor;
		}
	}
}
