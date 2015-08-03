using UnityEngine;
using System.Collections;
using Enums;

public abstract class ObjectMonoBehaviour : MonoBehaviour {

	bool isAttachedFireFly = false;

	public void AttachFireFly()
	{
		isAttachedFireFly = true;
	}

	public void DetachFireFly()
	{
		isAttachedFireFly = false;
	}

	public abstract void UpdateByParent();

	// DO NOT Implement 'Update' method in derived class.
	private void Update () {
		if ((isAttachedFireFly) && (Global.ingame.isDark == IsDark.Dark))
		{
			GetComponent<SpriteRenderer>().enabled = false;
			if (GetComponent<Box>() != null)
				GetComponent<Rigidbody2D>().isKinematic = true;
			GetComponent<Collider2D>().enabled = false;
		}
		else
			UpdateByParent();
	}
}
