using UnityEngine;
using System.Collections;
using Enums;

public class Penetrate : MonoBehaviour
{
	private BoxCollider2D coll;

	void Start()
	{
		coll = GetComponent<BoxCollider2D> ();
	}

	void Update()
	{
		if(Global.ingame.isDark == IsDark.Light)
		{
			coll.enabled = true;
		}
		else if(Global.ingame.isDark == IsDark.Dark)
		{
			coll.enabled = false;
		}
	}
}
