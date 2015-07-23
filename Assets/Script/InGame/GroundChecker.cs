using UnityEngine;
using System;
using System.Collections;
using Enums;

public class GroundChecker : MonoBehaviour {
	// collider name is duplicated with legacy api.
	new BoxCollider2D collider;
	void Start()
	{
		collider = GetComponent<BoxCollider2D> ();
	}

	public bool IsGrounded()
	{
		Bounds region = collider.bounds;
		return Physics2D.OverlapArea (region.max, region.min, LayerMask.GetMask ("Ground"));
	}
}
