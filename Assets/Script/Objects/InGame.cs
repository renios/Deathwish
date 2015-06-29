using UnityEngine;
using System.Collections;
using Enums;

public class InGame
{
	public IsDark isDark = IsDark.Light;

	public void ChangeDarkLight()
	{
		if(isDark == IsDark.Light)
		{
			isDark = IsDark.Dark;
		}

		else if(isDark == IsDark.Dark)
		{
			isDark = IsDark.Light;
		}
		Debug.Log ("Current World is " + isDark.ToString ());
	}
}
