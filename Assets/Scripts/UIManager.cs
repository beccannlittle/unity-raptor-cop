using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour {

	void Update () {
		if (Input.GetKeyDown (KeyCode.Escape)) {
			if (currentUI) {
				CloseCurrentUI ();
				hasHandledEsc = true;
			} else {
				hasHandledEsc = false;
			}
		}
	}

	// We want other UI scripts to be able to decide whether they open on Esc
	private bool hasHandledEsc;
	public bool getHasHandledEsc() {
		if (hasHandledEsc) return true;
		else return false;
	}
	public void setHasHandledEsc() {
		hasHandledEsc = true;
	}

	private GameObject previousUI;
	private GameObject currentUI;

	public void SetCurrentUI(GameObject newUI) {
		if (currentUI) {
			currentUI.SetActive (false);
			previousUI = currentUI;
		}
		currentUI = newUI;
		currentUI.SetActive (true);
	}

	public void CloseCurrentUI() {
		hasHandledEsc = true;
		if (previousUI) {
			currentUI.SetActive (false);
			currentUI = previousUI;
			previousUI = null;
			currentUI.SetActive (true);
		} else {
			currentUI.SetActive (false);
			currentUI = null;
		}
	}

}
