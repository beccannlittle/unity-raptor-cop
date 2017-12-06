using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EscapeMenu : MonoBehaviour {
	public GameObject escapeMenu;

	// Update is called once per frame
	void Update () {
		if(PressedEscape){
			escapeMenu.SetActive (!escapeMenu.activeSelf);
		}
	}


	public bool PressedEscape {
		get {
			return Input.GetKeyDown(KeyCode.Escape);
		}
	}
	//this.GetComponent<SavingTheGame> ().SaveGame ();
	//SceneManager.LoadScene ("TitleScreen");
}
