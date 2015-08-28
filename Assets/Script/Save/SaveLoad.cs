using UnityEngine;
using System.Collections;
using System;
using Enums;

public class SaveLoad
{
	private static string DARK = "DARK";
	private static string LIGHT = "LIGHT";

	public static void SaveClear(UI.LevelTag levelTag, Enums.IsDark isDark)
	{
		PlayerPrefs.SetInt(levelTag.ToString(), 1);
		PlayerPrefs.SetString("MODE-" + levelTag.ToString(), IsDarkToString(isDark));
	}

    private static string IsDarkToString(IsDark isDark)
    {
        if (isDark == Enums.IsDark.Dark)
		{
			return DARK;
		}
		else
		{
			return LIGHT;
		}
    }

    public static bool IsCleared(UI.LevelTag levelTag)
	{
		return PlayerPrefs.GetInt(levelTag.ToString()) == 1;
	}
	
	public static Enums.IsDark GetClearedMode(UI.LevelTag levelTag)
	{
		var stringResult = PlayerPrefs.GetString("MODE-" + levelTag.ToString());
		return IsDarkFromString(stringResult);
	}

    private static IsDark IsDarkFromString(string stringResult)
    {
        if (stringResult == DARK)
		{
			return Enums.IsDark.Dark;
		}
		else
		{
			return Enums.IsDark.Light;
		}
    }

    public static void AllClear()
    {
        PlayerPrefs.SetString("Clear", "Clear");
    }

	public static bool IsAllCleared()
	{
		return PlayerPrefs.GetString("Clear") == "Clear";
	}

    public static void DeleteAll()
    {
        PlayerPrefs.DeleteAll();
    }
}
