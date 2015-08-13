using UnityEngine;
using System.Collections;

public class floatable : MonoBehaviour {

	private Collider2D coll;
	private float initGravityScale;

	// Use this for initialization
	void Start () {
		coll = gameObject.GetComponent<Collider2D>();
		initGravityScale = GetComponent<Rigidbody2D>().gravityScale;
	}
	
	// Update is called once per frame
	void Update () {
		Collider2D waterCollider = GetWaterCollider();
		if (waterCollider != null)
		{
			float upperBoundOfWaterCollider = waterCollider.bounds.max.y;
			if (gameObject.transform.position.y <= upperBoundOfWaterCollider)
			{
				// Delete shake at surface.
				if (Mathf.Abs(gameObject.transform.position.y - upperBoundOfWaterCollider) < 0.1f)
				{
					transform.position = new Vector2(transform.position.x, upperBoundOfWaterCollider);
					GetComponent<Rigidbody2D>().velocity = Vector2.zero;
				}
				else
				{
					GetComponent<Rigidbody2D>().gravityScale = 0;
					GetComponent<Rigidbody2D>().velocity = new Vector2(0, 10);
				}
			}
			else
				GetComponent<Rigidbody2D>().gravityScale = initGravityScale;
		}
		else
			GetComponent<Rigidbody2D>().gravityScale = initGravityScale;	
	}

	Collider2D GetWaterCollider()
	{
		Collider2D[] otherColliders = Physics2D.OverlapAreaAll(coll.bounds.max, coll.bounds.min);
		foreach (Collider2D otherCollider in otherColliders)
		{
			if (otherCollider.gameObject.tag == "Water")
				return otherCollider;
		}
		return null;
	}
}
