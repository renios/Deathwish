using UnityEngine;
using System.Collections;

public class LadderCheckerDown : MonoBehaviour 
{
	public bool isDownLaddered;
	
	private Collider2D latestLadderCollider = null;
	
	void Update()
	{
		isDownLaddered = IsDownLaddered ();
	}

	public bool IsDownLaddered()
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
