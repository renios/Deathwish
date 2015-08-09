using UnityEngine;
using System;
using System.Collections;
using Enums;

public class LightingChecker : MonoBehaviour
{
	new BoxCollider2D collider;

	private Collider2D[] lightingEffectColliders;
	private int length;

	public GameObject[] lightingEffectObjects;

	void Start()
	{
		collider = GetComponent<BoxCollider2D> ();
		Bounds region = collider.bounds;
		lightingEffectColliders = Physics2D.OverlapAreaAll (region.max, region.min);
		length = lightingEffectColliders.Length;
		lightingEffectObjects = new GameObject[length];
		for(int i = 0; i < length; i++)
		{
			lightingEffectObjects[i] = lightingEffectColliders[i].gameObject;
		}
	}

	public bool IsEffectLighting()
	{
		return lightingEffectColliders != null;
	}
}
