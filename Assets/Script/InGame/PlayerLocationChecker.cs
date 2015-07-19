using UnityEngine;
using System;
using System.Collections;
using Enums;

public class PlayerLocationChecker : MonoBehaviour {

	private Action<Collider2D> onTriggerEnterWithGround;

	public void SetCallback(Action<Collider2D> onEnterGroundCallback)
	{
		this.onTriggerEnterWithGround = onEnterGroundCallback;
	}

	void OnTriggerEnter2D (Collider2D collision)
	{
		if(collision.gameObject.tag == "Ground")
		{
			onTriggerEnterWithGround(collision);
		}
	}
}
