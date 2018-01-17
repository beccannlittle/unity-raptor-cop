using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameConstants {
	//Scenes
	private static string Scene_TitleMenu_val = "TitleScreen";
	private static string Scene_World_val = "DavidsPlayground";
	private static string Scene_Credits_val = "Credits";

	//Game Resources
	private static string Resource_SaveGamePath_val = ""+ Application.persistentDataPath + "savegame.data";
	private static string Resource_SaveOptionsPath_val = ""+ Application.persistentDataPath + "gameAudioOptions.dat";
	private static string Resource_ScorePrefix_val = "Score: ";
	private static string Resource_SheepExistPrefix_val = "Sheep Remaining: ";

	//GameTags
	private static string Tag_Sheep_val = "Sheep";

	//Getters
	public static string Scene_TitleMenu { get { return Scene_TitleMenu_val; } }
	public static string Scene_World { get { return Scene_World_val; } }
	public static string Scene_Credits { get { return Scene_Credits_val; } }
	public static string Resource_SaveGamePath { get { return Resource_SaveGamePath_val; } }
	public static string Resource_SaveOptionsPath { get { return Resource_SaveOptionsPath_val; } }
	public static string Resource_ScorePrefix { get { return Resource_ScorePrefix_val; } }
	public static string Resource_SheepExistPrefix { get { return Resource_SheepExistPrefix_val; } }
	public static string Tag_Sheep { get { return Tag_Sheep_val; } }

}
