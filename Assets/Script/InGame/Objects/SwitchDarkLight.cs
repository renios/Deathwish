using UnityEngine;
using System.Collections;
using Enums;

public class SwitchDarkLight : MonoBehaviour
{
	void OnTriggerStay2D(Collider2D collision)
	{
		if ((collision.gameObject.tag == "Player") && (Input.GetKeyDown(KeyCode.UpArrow)))
		{
			Global.ingame.ChangeDarkLight();
		}
	}
}
