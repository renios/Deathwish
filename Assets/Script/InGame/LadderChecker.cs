using UnityEngine;
using System;
using System.Collections;
using Enums;

public class LadderChecker : MonoBehaviour
{
	public bool IsLaddered()
	{
		Bounds region = GetComponent<BoxCollider2D> ().bounds;
		return Physics2D.OverlapArea (region.max, region.min, LayerMask.GetMask ("Ladder"));
	}
}
