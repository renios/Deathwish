using UnityEngine;
using System.Collections.Generic;
using UnityEngine.UI;

public class LevelSelectManager : MonoBehaviour
{
	void Start()
	{
		var stageButtons = FindObjectsOfType<UI.StageButton>();
		foreach (var stageButton in stageButtons)
		{
			if (stageButton.IsLocked())
			{
				stageButton.Lock();
			}
			else
			{
				stageButton.Unlock();
			}
		}
		
		foreach (var stageButton in stageButtons)
		{
			Scene.AddStage(stageButton.GetMapName(), stageButton.GetLevelTag());
		}
	}

	void Update()
	{
		
	}

}
