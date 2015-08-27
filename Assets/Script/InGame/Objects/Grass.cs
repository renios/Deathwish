using UnityEngine;
using System.Collections;
using Enums;

public class Grass : ObjectMonoBehaviour
{
	SpriteRenderer spriteRenderer;
	GroundChecker groundChecker;
	Player player;
	
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

	void OnTriggerStay2D(Collider2D other)
	{
		groundChecker = other.gameObject.GetComponent<GroundChecker> ();
		if (groundChecker != null)
		{
			player = other.gameObject.GetComponentInParent<Player> ();
			if(player != null) player.withGrass = true;
		}
	}

	void OnTriggerExit2D(Collider2D other)
	{
		if (other.gameObject.GetComponent<GroundChecker> () != null)
			if(other.gameObject.GetComponentInParent<Player> () != null)
			{
				player.withGrass = false;
				groundChecker = null;
				player = null;
			}
	}
}
