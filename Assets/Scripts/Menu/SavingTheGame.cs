using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class SavingTheGame : MonoBehaviour {
	public string fileName = "savegame.data";

	void OnTriggerEnter(Collider col){
		SaveGame ();
	}
	public void SaveGame(){
		StreamWriter ourFile = File.CreateText (fileName);
		ourFile.WriteLine ("Saving the Game");
		GameObject player = GameObject.FindGameObjectWithTag ("Player");
		ourFile.WriteLine ("PlayerPosX="+player.transform.position.x);
		ourFile.WriteLine ("PlayerPosY="+player.transform.position.y);
		ourFile.WriteLine ("PlayerPosZ="+player.transform.position.z);
		ourFile.WriteLine ("PlayerRotateX="+player.transform.rotation.x);
		ourFile.WriteLine ("PlayerRotateY="+player.transform.rotation.y);
		ourFile.WriteLine ("PlayerRotateZ="+player.transform.rotation.z);

		ourFile.WriteLine ("load10coins");
		ourFile.Close ();
	}
}

