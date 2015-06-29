using UnityEngine;
using System.Collections;
using Enums;

public class InGame
{
	public isDark isdark = isDark.Light;

	public void ChangeDarkLight()
	{
		if(isdark == isDark.Light)
		{
			isdark = isDark.Dark;
		}

		else if(isdark == isDark.Dark)
		{
			isdark = isDark.Light;
		}
		Debug.Log ("Current World is " + isdark.ToString ());
	}
}
