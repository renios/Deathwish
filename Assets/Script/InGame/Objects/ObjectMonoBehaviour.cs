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

	public IsDark isDarkAfterLamp()
	{
		if(isNearbyLamp && nearbyLamp != null)
		{
			if(nearbyLamp.lampProperty == LampProperty.LightLamp)
			{
				return IsDark.Light;
			}
			else if(nearbyLamp.lampProperty == LampProperty.DarkLamp)
			{
				return IsDark.Dark;
			}
			else
			{
				return IsDark.Light;
			}
		}
		else if(!isNearbyLamp)
		{
			if(Global.ingame.isDark == IsDark.Light)
			{
				return IsDark.Light;
			}
			else if(Global.ingame.isDark == IsDark.Dark)
			{
				return IsDark.Dark;
			}
			else
			{
				return IsDark.Light;
			}
		}
		else
		{
			return IsDark.Light;
		}
	}

	public abstract void UpdateByParent();
	public abstract void HideObject();

	// DO NOT Implement 'Update' method in derived class.
	private void Update ()
	{
		//foreach(Lamp lamp in Global.ingame.LampsInMap)
		//{
			//return;
		//}

		SpriteSwitch ss = GetComponentInChildren<SpriteSwitch> ();
		if (ss != null)
			ss.isDark = isDarkAfterLamp ();

		if ((isAttachedFireFly) && (isDarkAfterLamp() == IsDark.Dark))
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
