using UnityEngine;
using System.Collections;
using Enums;

public class WorldControl
{
	public isDark World = isDark.Light;

	public void WorldChange()
	{
		if(World == isDark.Light)
		{
			World = isDark.Dark;
		}

		else if(World == isDark.Dark)
		{
			World = isDark.Light;
		}
		Debug.Log ("Current World is " + World.ToString ());
	}
}
