using UnityEngine;
using System.Collections;
using Enums;

public class SwitchDarkLight : MonoBehaviour
{
	private bool isGround;
	private bool collideWithPlayer = false;

	void Update()
	{
		isGround = GameObject.FindObjectOfType<GroundChecker> ().IsGrounded ();

		if (Input.GetKeyDown(KeyCode.UpArrow))
		{
			Global.ingame.ChangeDarkLight();
		}
	}

	void OnTriggerStay2D(Collider2D collision)
	{
		if (isGround && (collision.gameObject.tag == "Player"))
		{
			collideWithPlayer = true;
		}
		else
		{
			collideWithPlayer = false;
		}
	}
}
