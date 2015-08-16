using UnityEngine;
using System;
using System.Collections;
using System.Linq;

public class ObjectFinder
{
    public static Transform FindLowest()
    {
        return FindMost((previousResult, newData) => previousResult.y >= newData.y);
    }

    public static Transform FindRightmost()
    {
        return FindMost((previousResult, newData) => previousResult.x <= newData.x);
    }

    public static Transform FindLeftmost()
    {
        return FindMost((previousResult, newData) => previousResult.x >= newData.x);
    }

    public static Transform FindUpmost()
    {
        return FindMost((previousResult, newData) => previousResult.y <= newData.y);
    }

    private static Transform FindMost(Func<Vector3, Vector3, bool> comparator)
    {
        Transform transformOfLowestObject;

        Transform[] allTransforms = GameObject.FindObjectsOfType<ObjectMonoBehaviour>()
            .Select(objectMonoBehaviour => objectMonoBehaviour.GetComponent<Transform>()).ToArray();
        transformOfLowestObject = allTransforms[0];

        foreach (Transform transform in allTransforms)
        {
            if (comparator(transformOfLowestObject.position, transform.position))
            {
                transformOfLowestObject = transform;
            }
        }
        return transformOfLowestObject;
    }
}
