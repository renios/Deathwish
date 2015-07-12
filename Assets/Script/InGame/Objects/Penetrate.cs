using UnityEngine;
using System.Collections;
using Enums;

public class Penetrate : MonoBehaviour
{
	public Sprite normal;
	public Sprite transparent;
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
			gameObject.GetComponent<SpriteRenderer>().sprite = normal;
		}
		else if(Global.ingame.isDark == IsDark.Dark)
		{
			coll.enabled = false;
			gameObject.GetComponent<SpriteRenderer>().sprite = transparent;
		}
	}
}
