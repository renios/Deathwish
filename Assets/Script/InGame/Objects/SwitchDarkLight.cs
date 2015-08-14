using UnityEngine;
using System.Collections;
using Enums;

public class SwitchDarkLight : MonoBehaviour
{
	private bool isGround;

	void Update()
	{
		isGround = GameObject.Find ("GroundChecker").GetComponent<GroundChecker> ().IsGrounded ();
	}

	void OnTriggerStay2D(Collider2D collision)
	{
		if (isGround && (collision.gameObject.tag == "Player") && (Input.GetKeyDown(KeyCode.UpArrow)))
		{
			Global.ingame.ChangeDarkLight();
		}
	}
}
