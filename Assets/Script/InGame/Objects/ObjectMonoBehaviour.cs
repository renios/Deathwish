using UnityEngine;
using System.Collections;
using Enums;

public abstract class ObjectMonoBehaviour : MonoBehaviour
{
	public GravityDirection gravityDirection;

	bool isAttachedFireFly = false;

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
		return Global.ingame.GetIsDarkInPosition(gameObject);
	}

	public abstract void StartByParent();
	public abstract void UpdateByParent();
	public abstract void HideObject();

	// DO NOT Implement 'Start' method in derived class.
	private void Start ()
	{
		StartByParent ();
	}

	// DO NOT Implement 'Update' method in derived class.
	private void Update ()
	{
		if ((isAttachedFireFly) && (isDarkAfterLamp() == IsDark.Dark))
		{
			HideObject();
			if (GetComponent<Pushable>() != null)
				GetComponent<Rigidbody2D>().isKinematic = true;
		}
		else
			UpdateByParent();
	}
}
