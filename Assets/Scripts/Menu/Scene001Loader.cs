using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class Scene001Loader : MonoBehaviour {
	public GameObject player;
	public string fileName = "savegame.data";
	public float PlayerPosX;
	public float PlayerPosY;
	public float PlayerPosZ;
	public float PlayerRotX;
	public float PlayerRotY;
	public float PlayerRotZ;


	void Start(){
		if (LoadAndNew.GlobalLoad == "LoadGame") {
			LoadData ();
			setupPlayer ();
		}
	}
	void LoadData(){
		StreamReader ourFile = new StreamReader (fileName);

		string line = ourFile.ReadLine ();
		while(line != null){
			char[] delimiterEQ = new char[] {'='};
			string[] lineARR = line.Split(delimiterEQ);
			string fieldName = lineARR[0].Trim();
			string fieldValue = lineARR[1].Trim();
			switch(fieldName){
			case "PlayerPosX":
				PlayerPosX = float.Parse (fieldValue);
				break;
			default:
				Debug.Log ("Unknown Line in Game Save File: " + line);
				break;
			}

			line = ourFile.ReadLine ();
		}
		ourFile.Close ();
	}
	void setupPlayer(){
		Quaternion loadedQuatern = Quaternion.Euler (PlayerRotX, PlayerRotY, PlayerRotZ);
		Vector3 loadedPos = new Vector3 (PlayerPosX, PlayerPosY, PlayerPosZ);
		player.transform.SetPositionAndRotation (loadedPos, loadedQuatern);
	}
}
