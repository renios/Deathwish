using UnityEngine;
using System.Collections;
using System;

public class SaveLoad
{
	public static void SaveClear(UI.LevelTag levelTag, Enums.IsDark isDark)
	{
		PlayerPrefs.SetInt(levelTag.ToString(), 1);
	}
	
	public static bool IsCleared(UI.LevelTag levelTag)
	{
		return PlayerPrefs.GetInt(levelTag.ToString()) == 1;
	}

    public static void DeleteAll()
    {
        PlayerPrefs.DeleteAll();
    }
}
