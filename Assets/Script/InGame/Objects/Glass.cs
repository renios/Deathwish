using UnityEngine;
using System.Collections;

public class Glass : MonoBehaviour
{
	private BoxCollider2D coll;

	void Start()
	{
		coll = GetComponent<BoxCollider2D> ();
	}

	void Update()
	{
		if(Global.ingame.isDark == Enums.IsDark.Light)
		{
			coll.enabled = true;
		}
		else if(Global.ingame.isDark == Enums.IsDark.Dark)
		{
			coll.enabled = false;
		}
	}
}
