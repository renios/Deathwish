using UnityEngine;
using System.Collections;
using Enums;

public class SwitchDarkLight : MonoBehaviour
{
	private bool isGround;
	private bool isPlayer = false;

	void Update()
	{
		isGround = GameObject.FindObjectOfType<GroundChecker> ().IsGrounded ();

		if (isPlayer && Input.GetKeyDown(KeyCode.UpArrow) && isGround)
		{
			Global.ingame.ChangeDarkLight();
		}
	}

	void OnTriggerEnter2D(Collider2D player)
	{
		if (player.gameObject.tag == "Player")
		{
			isPlayer = true;
		}
	}

	void OnTriggerExit2D(Collider2D player)
	{
		if (player.gameObject.tag == "Player")
		{
			isPlayer = false;
		}
	}
}
