using UnityEngine;
using System.Collections;

public class UIButtonEventManager : MonoBehaviour
{
	public string NextLevel;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	
	public void ToTitle()
	{
		Scene.Load("Title", Scene.SceneType.Stage);
	}
	
	public void ToStageSelect()
	{
		Scene.Load("SelectStage", Scene.SceneType.Stage);
	}

	public void ToOption()
	{
		Scene.Load("Option", Scene.SceneType.Stage);
	}

	public void Restart()
	{
		Restarter.RestartAll();
	}
	
	public void GoToTestScene()
	{
		Scene.Load("Test", Scene.SceneType.Stage);
	}

	public void NewGame()
	{
		PlayerPrefs.DeleteAll();
	}
}
