using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EscapeMenu : MonoBehaviour {
	public GameObject optionsMenuPanel;
	public GameObject gameManager;

	public void QuitToMenu(bool shouldSaveGame){
		if(shouldSaveGame){
			gameManager.GetComponent<SavingTheGame> ().SaveGame ();
		}
		SceneManager.LoadScene ("TitleScreen");	
	}

	public void ToggleEscapeMenuVisibility(){
		gameObject.SetActive (!gameObject.activeSelf);
	}
	public void OpenOptionsMenu(){
		ToggleEscapeMenuVisibility ();
		optionsMenuPanel.SetActive (true);
	}
}
