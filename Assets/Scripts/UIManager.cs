using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour {

	private GameObject uiToOpenOnEsc;

	public void setUIToOpenOnEsc(GameObject escUI) {
		uiToOpenOnEsc = escUI;
	}

	void Update () {
		if (Input.GetKeyDown (KeyCode.Escape)) {
			if (currentUI) {
				CloseCurrentUI ();
			} else if (uiToOpenOnEsc) {
				SetCurrentUI (uiToOpenOnEsc);
			}
		}
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
