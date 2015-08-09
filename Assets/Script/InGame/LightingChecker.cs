using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;
using Enums;

public class LightingChecker : MonoBehaviour
{
	new BoxCollider2D collider;

	private Collider2D[] lightingEffectColliders;
	private int length;

	public HashSet<GameObject> lightingEffectObjects = new HashSet<GameObject>();

	void Start()
	{
		collider = GetComponent<BoxCollider2D> ();
	}

	void Update()
	{
		if (Global.ingame.isDark == IsDark.Light)
		{
			Bounds region = collider.bounds;
			lightingEffectColliders = Physics2D.OverlapAreaAll (region.max, region.min);
			length = lightingEffectColliders.Length;
			lightingEffectObjects = new HashSet<GameObject>();
			for (int i = 0; i < length; i++)
			{
				lightingEffectObjects.Add(lightingEffectColliders[i].gameObject);
			}
		}
	}

	public bool IsEffectLighting()
	{
		return lightingEffectColliders != null;
	}
}
