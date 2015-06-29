using UnityEngine;
using System.Collections;

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
	public static string currentSceneName = "WorkShop";
	public static SceneType currentSceneType = SceneType.Stage;

	static Scene()
	{
		AfterLoad ();
	}

	public static void Load(string sceneName, SceneType sceneType)
	{
		BeforeLoad ();
		currentSceneName = sceneName;
		currentSceneType = sceneType;
		Application.LoadLevel (sceneName);
		AfterLoad ();
	}

	private static void BeforeLoad()
	{
		Debug.Log ("Before load " + currentSceneName + ", type is " + currentSceneType.ToString());
		if(currentSceneType == SceneType.Stage)
		{
			Global.world = null;
		}
	}

	private static void AfterLoad()
	{
		Debug.Log ("After load " + currentSceneName + ", type is " + currentSceneType.ToString());
		if(currentSceneType == SceneType.Stage)
		{
			Global.world = new WorldControl ();
		}
	}
}
