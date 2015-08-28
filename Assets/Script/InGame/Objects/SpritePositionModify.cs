using UnityEngine;
using System.Collections;

public class SpritePositionModify : MonoBehaviour
{
	public float positionModifier;
	SpriteReverse spriteReverse;

	void Start()
	{
		spriteReverse = GetComponent<SpriteReverse> ();

		Debug.Log ("Before " + transform.position.y);
		Vector3 vec = new Vector3 (0, positionModifier, 0);
		if (spriteReverse.gravityDirection == Enums.GravityDirection.Normal)
		{
			gameObject.transform.position -= vec;
		}
		else
		{
			gameObject.transform.position += vec;
		}
		
		Debug.Log ("Aftr " + transform.position.y);
	}
}
