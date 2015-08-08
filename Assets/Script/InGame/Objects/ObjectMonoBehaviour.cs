using UnityEngine;
using System.Collections;
using Enums;

public abstract class ObjectMonoBehaviour : MonoBehaviour
{
	bool isAttachedFireFly = false;
	bool isNearbyLamp = false;
	Lamp nearbyLamp;

	public void AttachFireFly()
	{
		isAttachedFireFly = true;
	}

	public void DetachFireFly()
	{
		isAttachedFireFly = false;
	}

	void UpdateByParent2()
	{
		if(isNearbyLamp && nearbyLamp != null)
		{
			if(nearbyLamp.lampProperty == LampProperty.LightLamp)
			{
				//UpdateOnLight();
			}
			else if(nearbyLamp.lampProperty == LampProperty.DarkLamp)
			{
				//UpdateOnDark();
			}
		}
		else if(!isNearbyLamp)
		{
			if(Global.ingame.isDark == IsDark.Light)
			{
				//UpdateOnLight();
			}
			else if(Global.ingame.isDark == IsDark.Dark)
			{
				//UpdateOnDark();
			}
		}
		else
		{
			return;
		}
	}

	public abstract void UpdateByParent();
	//public abstract void UpdateOnLight();
	//public abstract void UpdateOnDark();
	public abstract void HideObject();

	// DO NOT Implement 'Update' method in derived class.
	private void Update ()
	{
		foreach(Lamp lamp in Global.ingame.LampsInMap)
		{
			return;
		}
		if ((isAttachedFireFly) && (Global.ingame.isDark == IsDark.Dark))
		{
			HideObject();
			//  GetComponent<SpriteRenderer>().enabled = false;
			if (GetComponent<Pushable>() != null)
				GetComponent<Rigidbody2D>().isKinematic = true;
			//  GetComponent<Collider2D>().enabled = false;
		}
		else
			UpdateByParent();
	}
}
