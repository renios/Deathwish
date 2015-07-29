using UnityEngine;
using System;
using System.Collections;
using Enums;

public class LadderChecker : MonoBehaviour
{
	private Collider2D latestLadderCollider = null;

	public bool IsLaddered()
	{
		return GetLadderCollider () != null;
	}

	public Collider2D GetLadderCollider()
	{
		Bounds region = GetComponent<BoxCollider2D> ().bounds;
		Collider2D newCollider = Physics2D.OverlapArea (region.max, region.min, LayerMask.GetMask ("Ladder"));
		if (newCollider != null)
		{
			latestLadderCollider = newCollider;
		}
		return newCollider;
	}

	public Collider2D GetLatestLadderCollider()
	{
		return latestLadderCollider;
	}
}
