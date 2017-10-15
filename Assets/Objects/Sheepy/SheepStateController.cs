using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SheepStateController : MonoBehaviour {
	public enum SheepStates
	{
		Idle, Graze, Startled, Petrified
	};
	public SheepStates sheepState;
	// Use this for initialization
	void Start () {
		sheepState = SheepStates.Idle;
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
