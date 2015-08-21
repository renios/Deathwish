using UnityEngine;
using System.Collections;

public class HoverFromGround : MonoBehaviour {

	public LayerMask groundMask;
	private float depth = 0.02f;

	void FixedUpdate() {
		var minY = GetComponent<BoxCollider2D>().bounds.min.y;
		var castResult = Physics2D.Raycast(new Vector2(transform.position.x, minY), Vector2.down, 0.5f, groundMask);
		if (castResult.collider == null)
		{
			return;
		}

		var diff = minY - castResult.point.y;
		if (0 < diff && diff < depth)
		{
			transform.position = new Vector3(transform.position.x, transform.position.y + diff, transform.position.z);
		}
	}
}
