using UnityEngine;
using System.Collections;
using Enums;

public class Penetrate : ObjectMonoBehaviour
{
	private BoxCollider2D coll;
	// Read at Editor (Inspector). (ex : is Transparent In Light / Dark)
	public IsDark isTransparentIn;

	void Start()
	{
		coll = GetComponent<BoxCollider2D> ();
	}

	public override void UpdateByParent()
	{
		GetComponent<SpriteRenderer>().enabled = true;

		if (Global.ingame.isDark == isTransparentIn)
		{
			coll.enabled = false;
		}
		else
		{
			coll.enabled = true;
		}
	}
}
