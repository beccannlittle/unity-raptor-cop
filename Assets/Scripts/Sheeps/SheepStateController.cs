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
	void Start ()
	{
		state = State.Idle;
		baseSheep = gameObject.GetComponent<BasicSheep>();
	}


	public void Update ()
	{
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
		CheckStatus ();
	}

	public void OnTriggerStay(){
		state = State.Startled;
	}

	public void OnTriggerExit(){
		state = State.Idle;
	}

	void CheckStatus(){
		if (baseSheep.health <= 0) {
			state = State.Dead;
		} else if (baseSheep.hunger <= 0) {
			state = State.Petrified;
		}
	}

	void DoIdleThings ()
	{
		//Perform Idle Code
		ChangeColor (Color.white);
		baseSheep.hunger = baseSheep.hunger - (baseSheep.hungerDecayRateAmount * Time.deltaTime);
		//Should I Leave Idle State?
		CheckForHungry ();
	}

	void DoGrazeThings ()
	{
		ChangeColor (Color.green);
		baseSheep.hunger = baseSheep.hunger + (baseSheep.hungerDecayRateAmount * Time.deltaTime);
		//Should I Leave Graze State?
		CheckForFull ();
	}

	void DoStartledThings ()
	{
		baseSheep.hunger = baseSheep.hunger - (baseSheep.hungerDecayRateAmount * Time.deltaTime);
		ChangeColor (Color.yellow);
	}

	void DoPetrifiedThings ()
	{
		baseSheep.health = baseSheep.health - (2.0f * Time.deltaTime);
		ChangeColor (Color.grey);
	}

	void DoDeadThings ()
	{
		ChangeColor (Color.black);
		Debug.Log ("A Sheep has Died");
		baseSheep.Die ();
	}
	public void CheckForHungry ()
	{
		if (baseSheep.hunger <= baseSheep.hungerThresholdMin) {
			Debug.Log ("TOO HUNGRY MUST EAT! Hunger: " + baseSheep.hunger);
			state = State.Graze;
		}
	}

	public void CheckForFull ()
	{
		if (baseSheep.hunger >= baseSheep.hungerThresholdMax) {
			Debug.Log ("Ahhhh nice and full! Hunger: " + baseSheep.hunger);
			state = State.Idle;
		}
	}

	public void CheckForStartled (State s)
	{
		GameObject player = GameObject.FindWithTag ("Player");
		float distanceToPlayer = Mathf.Abs (Vector3.Distance (player.transform.position, gameObject.transform.position));
		//Debug.Log ("Distance to Player: "+distanceToPlayer);
		if (distanceToPlayer < baseSheep.startledDistance) {
			Debug.Log ("The Raptor is near me! I'm so Startled!");
			state = State.Startled;
		} else {
			Debug.Log ("I Escaped!");
			state = s;
		}
	}

	public void ChangeColor (Color newcolor)
	{
		//Debug.Log ("I am being Idle");
		foreach (Renderer childRenderer in GetComponentsInChildren<Renderer>()) {
			childRenderer.material.color = newcolor;
			//Debug.Log ("Found Child Object: "+childRenderer.gameObject.name);
		}
	}
}
