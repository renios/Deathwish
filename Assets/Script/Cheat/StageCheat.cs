using UnityEngine;
using System.Collections;

public class StageCheat : MonoBehaviour {
	void Update () {
		if (Input.GetKey(KeyCode.Escape))
		{
			Scene.Load("Menu", Scene.SceneType.MainScene);
		}

		if (Input.GetKeyUp(KeyCode.N))
		{
			var allLevelTags = Scene.GetAllLevelTags();
			var levelTagIterator = allLevelTags.GetEnumerator();
			while (levelTagIterator.MoveNext())
			{
				SaveLoad.SaveClear(levelTagIterator.Current, Enums.IsDark.Dark);
				if (levelTagIterator.MoveNext())
				{
					SaveLoad.SaveClear(levelTagIterator.Current, Enums.IsDark.Light);	
				}
			}
		}
		
		if (Input.GetKeyUp(KeyCode.D))
		{
			var allLevelTags = Scene.GetAllLevelTags();
			var levelTagIterator = allLevelTags.GetEnumerator();
			while (levelTagIterator.MoveNext())
			{
				SaveLoad.SaveClear(levelTagIterator.Current, Enums.IsDark.Dark);
			}
		}

		if (Input.GetKeyUp(KeyCode.L))
		{
			var allLevelTags = Scene.GetAllLevelTags();
			var levelTagIterator = allLevelTags.GetEnumerator();
			while (levelTagIterator.MoveNext())
			{
				SaveLoad.SaveClear(levelTagIterator.Current, Enums.IsDark.Light);
			}
		}
	}
}
