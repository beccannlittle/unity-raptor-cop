using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameConstants {
	//Scenes
	private static string SCENE_TITLEMENU_VAL = "TitleScreen";
	private static string SCENE_WORLD_001_VAL = "DavidsPlayground";
	private static string SCENE_CREDITS_VAL = "Credits";

	//Game Resources
	private static string RESOURCE_SAVEGAME_PATH_VAL = ""+ Application.persistentDataPath + "savegame.data";
	private static string RESOURCE_SAVEOPTIONS_PATH_VAL = ""+ Application.persistentDataPath + "gameAudioOptions.dat";
	private static string RESOURCE_SCOREPREFIX_VAL = "Score: ";
	private static string RESOURCE_SHEEPEXISTPREFIX_VAL = "Sheep Remaining: ";

	//GameTags
	private static string TAG_SHEEP_VAL = "Sheep";

	//Getters
	public static string SCENE_TITLEMENU { get { return SCENE_TITLEMENU_VAL; } }
	public static string SCENE_WORLD_001 { get { return SCENE_WORLD_001_VAL; } }
	public static string SCENE_CREDITS { get { return SCENE_CREDITS_VAL; } }
	public static string RESOURCE_SAVEGAME_PATH { get { return RESOURCE_SAVEGAME_PATH_VAL; } }
	public static string RESOURCE_SAVEOPTIONS_PATH { get { return RESOURCE_SAVEOPTIONS_PATH_VAL; } }
	public static string RESOURCE_SCOREPREFIX { get { return RESOURCE_SCOREPREFIX_VAL; } }
	public static string RESOURCE_SHEEPEXISTPREFIX { get { return RESOURCE_SHEEPEXISTPREFIX_VAL; } }
	public static string TAG_SHEEP { get { return TAG_SHEEP_VAL; } }

}
