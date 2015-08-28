using UnityEngine;
using System.Collections;

public class SpritePositionModify : MonoBehaviour
{
	public float positionModifier;
	SpriteReverse spriteReverse;

	void Start()
	{
		spriteReverse = GetComponent<SpriteReverse> ();

		Vector3 vec = new Vector3 (0, positionModifier, 0);
		if (spriteReverse.gravityDirection == Enums.GravityDirection.Normal)
		{
			gameObject.transform.position -= vec;
		}
		else
		{
			gameObject.transform.position += vec;
		}
	}
}
