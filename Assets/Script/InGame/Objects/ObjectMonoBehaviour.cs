using UnityEngine;
using System.Collections;
using Enums;

public abstract class ObjectMonoBehaviour : MonoBehaviour
{
	bool isAttachedFireFly = false;
	Lamp nearbyLamp = null;

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
		return Global.ingame.GetIsDarkInPosition(transform.position);
	}

	public abstract void UpdateByParent();
	public abstract void HideObject();

	// DO NOT Implement 'Update' method in derived class.
	private void Update ()
	{
		SpriteSwitch ss = GetComponentInChildren<SpriteSwitch> ();
		if (ss != null)
		{
			ss.isDark = isDarkAfterLamp ();
			ss.changed = true;
		}

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
