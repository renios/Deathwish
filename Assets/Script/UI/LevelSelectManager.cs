using UnityEngine;
using System.Collections.Generic;
using UnityEngine.UI;

public class LevelSelectManager : MonoBehaviour
{
	public List<UI.StageButton> stageButtons;

	void Start()
	{
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
	}

	void Update()
	{
		
	}

}
