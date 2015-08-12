using UnityEngine;
using System.Collections;
using Enums;

public class SwitchDarkLight : MonoBehaviour
{
	public GroundChecker groundChecker;

	void OnTriggerStay2D(Collider2D collision)
	{
		if (groundChecker.IsGrounded() && (collision.gameObject.tag == "Player") && (Input.GetKeyDown(KeyCode.UpArrow)))
		{
			Global.ingame.ChangeDarkLight();
		}
	}
}
