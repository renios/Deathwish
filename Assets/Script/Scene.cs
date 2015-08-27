using UnityEngine;
using System.Collections.Generic;
using System.Linq;
using UI;

public class Scene
{
	public enum SceneType
	{
		MainScene,
		StageSelect,
		Stage
	}

	//FIXME : set default value as main scene after problem solved
	//set default value as stage for test
	public static MapName currentSceneName = new MapName("WorkShop");
	public static SceneType currentSceneType = SceneType.Stage;

	static Scene()
	{
		AfterLoad ();
	}

	public static void Load(string sceneName, SceneType sceneType)
	{
		currentSceneName = new MapName(sceneName);
		currentSceneType = sceneType;
		Application.LoadLevel (sceneName);
		BeforeLoad (); // global.ingame should be null after loading scene.
		AfterLoad ();
	}

    public static void LoadNextStageAndSave()
    {
		try
		{
			var levelTag = mapNameToLevelTag[currentSceneName];
			PlayerPrefs.SetInt(levelTag.ToString(), 1);
			var nextLevelTag = GetNextLevelTag();

			if (nextLevelTag == null)
			{
				Debug.Log("Clear all stage!");
				Load("Title", SceneType.MainScene);
			}
			else
			{
				var nextSceneLevel = levelTagToMapName[nextLevelTag.Value];
				Load(nextSceneLevel.ToString(), SceneType.Stage);
			}
		}
		catch (KeyNotFoundException e)
		{
			Debug.LogException(e);
			Debug.LogError("You can go next scene only start from select stage.");
			throw;
		}
    }

    private static LevelTag? GetNextLevelTag()
    {
		IEnumerable<LevelTag> levelTags = levelTagToMapName.Keys;
        var nextLevels = levelTags.SkipWhile(levelTag => levelTag.CompareTo(currentLevelTag) <= 0)
			.ToList();

		if (nextLevels.Count > 0)
		{
			return nextLevels.First();
		} else {
			return null;
		}
    }
	
	public static LevelTag? GetPreviousLevelTag(LevelTag current)
	{
		IEnumerable<LevelTag> levelTags = levelTagToMapName.Keys.Reverse();
		var nextLevels = levelTags.SkipWhile(levelTag => levelTag.CompareTo(current) >= 0)
			.ToList();

		if (nextLevels.Count > 0)
		{
			return nextLevels.First();
		} else {
			return null;
		}
	}

    private static Dictionary<MapName, LevelTag> mapNameToLevelTag = new Dictionary<MapName, LevelTag>();
	private static SortedDictionary<LevelTag, MapName> levelTagToMapName = new SortedDictionary<LevelTag, MapName>();

    public static LevelTag currentLevelTag {
		 get {
			 return mapNameToLevelTag[currentSceneName];
		 } 
	}

    public static void AddStage(MapName mapName, LevelTag levelTag)
    {
		mapNameToLevelTag[mapName] = levelTag;
		levelTagToMapName[levelTag] = mapName;
    }

    private static void BeforeLoad()
	{
		Debug.Log ("Before load " + currentSceneName + ", type is " + currentSceneType.ToString());
		if(currentSceneType == SceneType.Stage)
		{
			Global.ingame = null;
		}
	}

	private static void AfterLoad()
	{
		Debug.Log ("After load " + currentSceneName + ", type is " + currentSceneType.ToString());
		if(currentSceneType == SceneType.Stage)
		{
			Global.ingame = new InGame ();
		}
	}
}
