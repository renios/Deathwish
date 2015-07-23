using UnityEngine;
using System.Collections;

public class LowestObjectFinder
{
	public static Transform Find ()
	{
		Transform transformOfLowestObject;

		Transform[] allTransforms = GameObject.FindObjectsOfType<Transform> ();
		transformOfLowestObject = allTransforms [0];

		foreach (Transform transform in allTransforms)
		{
			if (transformOfLowestObject.position.y >= transform.position.y)
			{
				transformOfLowestObject = transform;
			}
		}
		return transformOfLowestObject;
	}
}
