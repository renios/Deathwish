using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Enums;

public class InGame
{
	public IsDark isDark = IsDark.Light;
	public List<Lamp> LampsInMap = new List<Lamp>();

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
	}
}
