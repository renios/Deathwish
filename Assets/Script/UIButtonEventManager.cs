using UnityEngine;
using System.Collections;

public class UIButtonEventManager : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	
	public void ToTitle()
	{
		Application.LoadLevel("Title");
	}
	
	public void ToStageSelect()
	{
		Application.LoadLevel("SelectStage");
	}
	
	public void ToOption()
	{
		Application.LoadLevel("Option");
	}

	public void Restart()
	{
		Restarter.RestartAll();
	}
	
	public void GoToTestScene()
	{
		Application.LoadLevel("Test");
	}
}
