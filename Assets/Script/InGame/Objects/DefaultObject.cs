using UnityEngine;
using System.Collections;

public class DefaultObject : ObjectMonoBehaviour {

	public override void StartByParent ()
	{
		return;
	}

	// Update is called once per frame
	public override void UpdateByParent ()
	{
		GetComponent<SpriteRenderer>().enabled = true;
		foreach (Collider2D collider in GetComponents<Collider2D>())
			collider.enabled = true;
	}

	public override void HideObject()
	{
		GetComponent<SpriteRenderer>().enabled = false;
		foreach (Collider2D collider in GetComponents<Collider2D>())
			collider.enabled = false;
	}
}
