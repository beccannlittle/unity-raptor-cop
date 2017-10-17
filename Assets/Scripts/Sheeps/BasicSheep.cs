using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicSheep : MonoBehaviour {
	public float hunger = 50.0f;
	public float health = 15.0f;
	private float hungerDecayRateAmount = 10.0f;
	public float hungerDecayRateTime = 3.0f;
	public float hungerThresholdMin = 30.0f;
	public float hungerThresholdMax = 100.0f;
	// Use this for initialization
	void Start () {
		StartCoroutine (HungerDecay());	
	}
	
	// Update is called once per frame
	void Update () {
		
		if (hunger <= hungerThresholdMin) {
			Debug.Log ("TOO HUNGRY MUST EAT!");
			gameObject.GetComponent<SheepStateController> ().state = SheepStateController.State.Graze;
		} else if (hunger >= hungerThresholdMax) {
			Debug.Log ("Ahhhh nice and full!");
			gameObject.GetComponent<SheepStateController> ().state = SheepStateController.State.Idle;
		}

	}

	IEnumerator HungerDecay(){
		while(health > 0.0f && hunger > 0.0f){
			float oldHunger = hunger;
			hunger = oldHunger - hungerDecayRateAmount;
			Debug.Log ("HungerDecays from ["+oldHunger+"] to ["+hunger+"]");
			yield return new WaitForSeconds(hungerDecayRateTime);
		}
		Debug.Log ("Death by Starvation");
	}

}
