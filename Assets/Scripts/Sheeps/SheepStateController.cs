using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SheepStateController : MonoBehaviour {
	public enum State {
		Idle,
		Graze,
		Startled,
		Petrified,
		Dead
	}
	public State state;

	IEnumerator IdleState(){
		while (state.Equals (State.Idle)) {
			//Perform Idle Code
			ChangeColor (Color.white);
			gameObject.GetComponent<BasicSheep> ().hunger = gameObject.GetComponent<BasicSheep> ().hunger - gameObject.GetComponent<BasicSheep> ().hungerDecayRateAmount;
			//Should I Leave Idle State?
			if (gameObject.GetComponent<BasicSheep>().hunger <= gameObject.GetComponent<BasicSheep>().hungerThresholdMin) {
				Debug.Log ("TOO HUNGRY MUST EAT! Hunger: "+gameObject.GetComponent<BasicSheep>().hunger);
				ChangeState(State.Graze);
			}
		
			yield return new WaitForSeconds(2.0f);
		}
	}
	IEnumerator GrazeState(){
		while(state.Equals(State.Graze)){
			ChangeColor (Color.green);
			gameObject.GetComponent<BasicSheep> ().hunger = gameObject.GetComponent<BasicSheep> ().hunger + gameObject.GetComponent<BasicSheep> ().hungerDecayRateAmount;
			//Should I Leave Graze State?
			if (gameObject.GetComponent<BasicSheep>().hunger >= gameObject.GetComponent<BasicSheep>().hungerThresholdMax) {
				Debug.Log ("Ahhhh nice and full! Hunger: "+gameObject.GetComponent<BasicSheep>().hunger);
				ChangeState(State.Idle);
			}
			yield return new WaitForSeconds(2.0f);
		}
	}
	IEnumerator StartledState(){
		while(state.Equals(State.Startled)){
			ChangeColor (Color.yellow);
			yield return new WaitForSeconds(2.0f);
		}
	}
	IEnumerator PetrifiedState(){
		while(state.Equals(State.Petrified)){
			ChangeColor (Color.grey);
			yield return new WaitForSeconds(2.0f);
		}
	}
	IEnumerator DiedState(){
		while(state.Equals(State.Dead)){
			ChangeColor (Color.black);
			yield return new WaitForSeconds(2.0f);
		}
	}

	void Start () {
		state = State.Idle;
		StartCoroutine (IdleState ());
	}

	public void ChangeState(State s){
		Debug.Log ("Changing State from: "+state+" to: "+s);
		if (state != s) { 
			StopAllCoroutines ();
			state = s;
			if (s == State.Idle) {
				StartCoroutine (IdleState ());
			} else if (s == State.Graze) {
				StartCoroutine (GrazeState ());
			} else if (s == State.Startled) {
				StartCoroutine (StartledState ());
			} else if (s == State.Petrified) {
				StartCoroutine (PetrifiedState ());
			} else if (s == State.Dead) {
				StartCoroutine (DiedState ());
			} else {
				Debug.Log ("Unknown Sheep State: "+s+" Is not a known state!");
			}
		}
	}

	public void ChangeColor(Color newcolor){
		//Debug.Log ("I am being Idle");
		foreach (Renderer childRenderer in GetComponentsInChildren<Renderer>()) {
			childRenderer.material.color = newcolor;
			//Debug.Log ("Found Child Object: "+childRenderer.gameObject.name);
		}
	}
}
