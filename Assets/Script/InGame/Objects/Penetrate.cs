using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Enums;

public class Penetrate : ObjectMonoBehaviour
{
	private List<Collider2D> collider2Ds;
	// Read at Editor (Inspector). (ex : is Transparent In Light / Dark)
	public IsDark isTransparentIn;

	void Start()
	{
		collider2Ds = new List<Collider2D>(GetComponents<Collider2D> ());
	}

	public override void UpdateByParent()
	{
		GetComponent<SpriteRenderer>().enabled = true;
		if (isDarkAfterLamp() == isTransparentIn)
		{
			foreach (var collider in collider2Ds)
			{
				collider.enabled = false;
			}
		}
		else
		{
			foreach (var collider in collider2Ds)
			{
				collider.enabled = true;
			}
		}
	}

	public override void HideObject()
	{
		GetComponent<SpriteRenderer>().enabled = false;

		foreach (var collider in collider2Ds)
		{
			collider.enabled = false;
		}
	}
}
