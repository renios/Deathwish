using UnityEngine;
using System.Collections;
using Enums;

public class Penetrate : MonoBehaviour
{
	public Sprite inLight;
	public Sprite inDark;

	private BoxCollider2D coll;
	// Read at Editor (Inspector). (ex : is Transparent In Light / Dark)
	public IsDark isTransparentIn;

	void Start()
	{
		coll = GetComponent<BoxCollider2D> ();
	}

	void Update()
	{
		if(Global.ingame.isDark == IsDark.Light)
		{
			gameObject.GetComponent<SpriteRenderer>().sprite = inLight;
		}
		else if(Global.ingame.isDark == IsDark.Dark)
		{
			gameObject.GetComponent<SpriteRenderer>().sprite = inDark;
		}

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
