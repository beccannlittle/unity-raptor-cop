using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConstantsManager : MonoBehaviour {

	[Header("Scene names")]
	[SerializeField]
	private string titleScreen = "TitleScreen";
	[SerializeField]
	private string[] levels;
	[SerializeField]
	private string credits = "Credits";
	[SerializeField]
	private string saveGamePath = "savegame.data";

	public string getTitleScreen() { return titleScreen; }
	public string getLevel(int index) { 
		if (levels.Length > 0) {
			return levels [index]; 
		} else {
			Debug.Log ("You forgot to set a level in the GameController");
			return titleScreen;
		}
	}
	public string getCredits() { return credits; }

	public string getSaveGamePath() {
		return "" + Application.persistentDataPath + saveGamePath;
	}

}
