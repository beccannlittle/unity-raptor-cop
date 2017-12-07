using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EscapeMenu : MonoBehaviour {
	public GameObject escapeMenuPanel;
	public GameObject optionsMenuPanel;
	public GameObject gameManager;

	// Update is called once per frame
	void Update () {
		if(PressedEscape){
			ToggleEscapeMenuVisibility ();
		}
	}


	public bool PressedEscape {
		get {
			return Input.GetKeyDown(KeyCode.Escape);
		}
	}
	public void SaveGame(){
		Debug.Log ("[GameManager]: Saving the game");
		gameManager.GetComponent<SavingTheGame> ().SaveGame ();

	}
	public void QuitToMenu(bool shouldSaveGame){
		if(shouldSaveGame){
			SaveGame();
		}
		SceneManager.LoadScene ("TitleScreen");	
	}

	public void ToggleEscapeMenuVisibility(){
		escapeMenuPanel.SetActive (!escapeMenuPanel.activeSelf);
	}
	public void ToggleOptionsMenuVisibility(bool shouldSaveOptions){
		if(shouldSaveOptions){
			SaveOptions ();
		}
		optionsMenuPanel.SetActive(optionsMenuPanel.activeSelf)
	}
	public void SaveOptions(){
		Debug.Log ("[GameManager]: Saving the game");
		//Options newoptions;
		//gameManager.GetComponent<Options> ().SaveOptions (newoptions);

	}
}
