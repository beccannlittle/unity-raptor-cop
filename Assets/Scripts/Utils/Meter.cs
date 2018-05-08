using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Meter {
	public float val;
	public float initialVal;
	public float maxVal;
	public float minVal;
	public float maxThreshold;
	public float minThreshold;

	//BillboardDisplay
	public RectTransform healthbar;

	public Meter(float initval,float minv,float maxv, float minthresh, float maxthresh){
		this.val = initval;
		this.initialVal = initval;
		this.minVal = minv;
		this.minThreshold = minthresh;
		this.maxVal = maxv;
		this.maxThreshold = maxthresh;
	}

	public void resetMeter(){
		SetVal(initialVal);
	}

	public float addVal(float f){
		//returns any excess value.
		float newVal = val + f; 
		if (newVal < minVal) {
			SetVal(minVal);
			return newVal - minVal;
		} else if (newVal > maxVal) {
			SetVal(maxVal);
			return newVal - maxVal;
		} else {
			SetVal(newVal);
			return 0.0f;
		}
	}
	public bool AboveMaxThreshold(){
		//inclusive
		return (val >= maxThreshold);
	}
	public bool BelowMaxThreshold(){
		//inclusive
		return (val <= maxThreshold);
	}
	public bool AboveMinThreshold(){
		//inclusive
		return (val >= minThreshold);
	}
	public bool BelowMinThreshold(){
		//inclusive
		return (val <= minThreshold);
	}
	public void SetToMin(){
		SetVal(minVal);
	}
	public void SetToMinThreshold(){
		SetVal(minThreshold);
	}
	public void SetToMax(){
		SetVal(maxVal);
	}
	public void SetToMaxThreshold(){
		SetVal(maxThreshold);
	}
	public float GetVal(){
		return val;
	}
	public void SetVal(float newVal){
		val = newVal;
		UpdateHealthBar ();
	}

	public void UpdateHealthBar(){
		if(healthbar != null){
			healthbar.sizeDelta = new Vector2 (val, healthbar.sizeDelta.y);
		}
	}

}