using UnityEngine;
using System.Collections;
using Enums;

public class Unpenetrate : MonoBehaviour
{
	public Sprite inlight;
	public Sprite indark;
	private BoxCollider2D coll;
	
	void Start()
	{
		coll = GetComponent<BoxCollider2D> ();
	}
	
	void Update()
	{
		if(Global.ingame.isDark == IsDark.Light)
		{
			coll.enabled = false;
			gameObject.GetComponent<SpriteRenderer>().sprite = inlight;
		}
		else if(Global.ingame.isDark == IsDark.Dark)
		{
			coll.enabled = true;
			gameObject.GetComponent<SpriteRenderer>().sprite = indark;
		}
	}
}
