using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuActions : MonoBehaviour {
	public GameObject escapeMenu;

	void Update () {
		if(PressedEscape){
			escapeMenu.GetComponent<EscapeMenu>().ToggleEscapeMenuVisibility ();
		}
	}

	public bool PressedEscape {
		get {
			return Input.GetKeyDown(KeyCode.Escape);
		}
	}
}
