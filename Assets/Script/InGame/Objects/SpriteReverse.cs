using UnityEngine;
using System.Collections;
using Enums;

public class SpriteReverse : MonoBehaviour
{
	public GravityDirection gravityDirection;

	void Update ()
	{
		if(gravityDirection == GravityDirection.Reverse)
			gameObject.transform.rotation = Quaternion.Euler (180, 0, 0);
	}
}
