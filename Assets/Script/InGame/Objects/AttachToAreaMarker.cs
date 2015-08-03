using UnityEngine;
using System.Collections;
using Enums;

public class AttachToAreaMarker : MonoBehaviour {

	private ObjectType parentObjectType;

	public void SetParentObjectType(ObjectType parentObjectType)
	{
		this.parentObjectType = parentObjectType;
	}

	void OnTriggerEnter2D(Collider2D other)
	{
		if (other.gameObject.tag == "AreaMarker")
		{
			GameObject areaMarker = other.gameObject;
			if (parentObjectType == ObjectType.LightBug)
			{
				foreach(Collider2D colliderOfOther in Physics2D.OverlapAreaAll(areaMarker.GetComponent<Collider2D>().bounds.max, areaMarker.GetComponent<Collider2D>().bounds.min))
				{
					if ((colliderOfOther.gameObject.GetComponent<ObjectMonoBehaviour>() != null) && (colliderOfOther.gameObject.tag != "Player"))
						colliderOfOther.gameObject.GetComponent<ObjectMonoBehaviour>().AttachLightBug();
				}
			}
		}
	}

	void OnTriggerExit2D(Collider2D other)
	{
		if (other.gameObject.tag == "AreaMarker")
		{
			GameObject areaMarker = other.gameObject;
			if (parentObjectType == ObjectType.LightBug)
			{
				foreach(Collider2D colliderOfOther in Physics2D.OverlapAreaAll(areaMarker.GetComponent<Collider2D>().bounds.max, areaMarker.GetComponent<Collider2D>().bounds.min))
				{
					if ((colliderOfOther.gameObject.GetComponent<ObjectMonoBehaviour>() != null) && (colliderOfOther.gameObject.tag != "Player"))
						colliderOfOther.gameObject.GetComponent<ObjectMonoBehaviour>().DetachLightBug();
				}
			}
		}
	}

}
