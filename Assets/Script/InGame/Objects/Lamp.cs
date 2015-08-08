using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Enums;

public class Lamp : ObjectMonoBehaviour
{
	public LampProperty lampProperty;
	public float detectingRadius;
	List<Lamp> lamps;

	void Start()
	{
		Global.ingame.LampsInMap.Add (this);
	}

	public override void UpdateByParent ()
	{
		GetComponent<SpriteRenderer> ().enabled = true;
		GetComponent<Collider2D> ().enabled = true;
	}
	
	public override void HideObject()
	{
		GetComponent<SpriteRenderer>().enabled = false;
		GetComponent<Collider2D>().enabled = false;
	}
}
