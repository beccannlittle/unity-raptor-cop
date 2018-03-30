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

	public Meter(float initval,float minv,float maxv, float minthresh, float maxthresh){
		this.val = initval;
		this.initialVal = initval;
		this.minVal = minv;
		this.minThreshold = minthresh;
		this.maxVal = maxv;
		this.maxThreshold = maxthresh;
	}

	public void resetMeter(){
		val = initialVal;
	}

	public float addVal(float f){
		//returns any excess value.
		float newVal = val + f; 
		if (newVal < minVal) {
			val = minVal;
			return newVal - minVal;
		} else if (newVal > maxVal) {
			val = maxVal;
			return newVal - maxVal;
		} else {
			val = newVal;
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
		val = minVal;
	}
	public void SetToMinThreshold(){
		val = minThreshold;
	}
	public void SetToMax(){
		val = maxVal;
	}
	public void SetToMaxThreshold(){
		val = maxThreshold;
	}
}