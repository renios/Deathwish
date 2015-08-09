using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Enums;

public class InGame
{
	public IsDark isDark = IsDark.Light;
	public HashSet<Lamp> LampsInMap = new HashSet<Lamp>();

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

	public IsDark GetIsDarkInPosition(Vector3 position)
	{
		Lamp nearbyLamp = null;
		foreach(Lamp lamp in Global.ingame.LampsInMap)
		{
			Vector3 difference = lamp.transform.position - position;
			if(difference.magnitude < lamp.detectingRadius)
			{
				nearbyLamp = lamp;
			}
		}

		if (nearbyLamp != null)
		{
			if(nearbyLamp.lampProperty == LampProperty.LightLamp)
			{
				return IsDark.Light;
			}
			else
			{
				return IsDark.Dark;
			}
		}
		else
		{
			if(Global.ingame.isDark == IsDark.Light)
			{
				return IsDark.Light;
			}
			else
			{
				return IsDark.Dark;
			}
		}
	}
}
