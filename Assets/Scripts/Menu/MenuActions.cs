using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuActions : MonoBehaviour {
	public GameObject escapeMenu;
	public GameObject optionsMenu;

	void Update () {
		if(!optionsMenu.activeSelf && PressedEscape){
			escapeMenu.GetComponent<EscapeMenu>().ToggleEscapeMenuVisibility ();
		}
	}

	public bool PressedEscape {
		get {
			return Input.GetKeyDown(KeyCode.Escape);
		}
	}
}
