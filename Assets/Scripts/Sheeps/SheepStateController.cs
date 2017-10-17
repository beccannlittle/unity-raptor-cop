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
		while(state.Equals(State.Idle)){
			ChangeColor (Color.white);
			yield return new WaitForSeconds(2.0f);
		}
	}
	IEnumerator GrazeState(){
		while(state.Equals(State.Graze)){
			ChangeColor (Color.green);
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

	void ChangeColor(Color newcolor){
		//Debug.Log ("I am being Idle");
		foreach (Renderer childRenderer in GetComponentsInChildren<Renderer>()) {
			childRenderer.material.color = newcolor;
			//Debug.Log ("Found Child Object: "+childRenderer.gameObject.name);
		}
	}

}
