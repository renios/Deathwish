using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Enums;

public class Lamp : ObjectMonoBehaviour
{
	public LampProperty lampProperty;
	public float detectingRadius;
	//forDebugging
	List<Lamp> lamps;
	Scaler scaler;

	void Start()
	{
		Global.ingame.LampsInMap.Add (this);
		lamps = Global.ingame.LampsInMap;
		scaler = GetComponentInChildren<Scaler> ();
		scaler.detectingRadius = detectingRadius;
	}

	public override void UpdateByParent ()
	{
		GetComponent<SpriteRenderer> ().enabled = true;
		GetComponent<Collider2D> ().enabled = true;
	}
	
	public override void HideObject()
	{
		return;
	}
}
