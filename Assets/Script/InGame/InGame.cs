using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Enums;

public class InGame
{
	public IsDark isDark = IsDark.Light;
	public HashSet<Lamp> LampsInMap = new HashSet<Lamp>();
	public HashSet<Lighting> LightsInMap = new HashSet<Lighting>();

	public bool inWind;

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

	public IsDark GetIsDarkInPosition(GameObject gameObject)
	{
		foreach (Lighting light in LightsInMap)
		{
			Debug.Assert(light != null);
			if (light.GetGameObjectsInLighting().Contains(gameObject))
			{
				return DecorateByDarkSplitter(IsDark.Light, gameObject.transform.position);
			}
		}
		return GetIsDarkInPosition(gameObject.transform.position);
	}

	public IsDark GetIsDarkInPosition(Vector3 position)
	{
		var firstIsDark = GetGlobalIsDark();
		var afterApplyLamp = DecorateByLamp(firstIsDark, position);
		var afterSplitter = DecorateByDarkSplitter(afterApplyLamp, position);
		return afterSplitter;
	}
	
	private IsDark DecorateByDarkSplitter(IsDark previousIsDark, Vector3 position)
	{
		var splitter = Chapter5.DarkSplitter.Instance;
		if (splitter != null)
		{
			if (position.y < splitter.transform.position.y)
			{
				//  Debug.Log("Reversed.");
				return ReverseIsDark(previousIsDark);
			}
		}
		return previousIsDark;
	}
	
	private IsDark DecorateByLamp(IsDark previousIsDark, Vector3 position)
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
			return previousIsDark;
		}
	}
	
	private IsDark GetGlobalIsDark()
	{
		return isDark;
	}

    public Player GetPlayer()
    {
        return GameObject.FindObjectOfType<Player>();
    }
		
	private static IsDark ReverseIsDark(IsDark isdark)
	{
		if (isdark == IsDark.Dark)
		{
			return IsDark.Light;
		}
		else
		{
			return IsDark.Dark;
		}
	} 
}
