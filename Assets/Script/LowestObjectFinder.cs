﻿using UnityEngine;
using System.Collections;
using System.Linq;

public class ObjectFinder
{
	public static Transform FindLowest ()
	{
		Transform transformOfLowestObject;

		Transform[] allTransforms = GameObject.FindObjectsOfType<ObjectMonoBehaviour> ()
			.Select(objectMonoBehaviour => objectMonoBehaviour.GetComponent<Transform>()).ToArray();
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
