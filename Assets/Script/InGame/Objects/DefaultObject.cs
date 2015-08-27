using UnityEngine;
using System.Collections;

public class DefaultObject : ObjectMonoBehaviour {

	SpriteRenderer spriteRenderer;

	public override void StartByParent ()
	{
		spriteRenderer = GetComponent<SpriteRenderer> ();
		if (spriteRenderer == null)
			spriteRenderer = GetComponentInChildren<SpriteRenderer> ();
	}

	// Update is called once per frame
	public override void UpdateByParent ()
	{
		spriteRenderer.enabled = true;
		foreach (Collider2D collider in GetComponents<Collider2D>())
			collider.enabled = true;
	}

	public override void HideObject()
	{
		spriteRenderer.enabled = false;
		foreach (Collider2D collider in GetComponents<Collider2D>())
			collider.enabled = false;
	}
}
