using UnityEngine;
using System.Collections;
using Enums;

public class WorldControl : MonoBehaviour
{
	public isDark World;

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
	}
}
