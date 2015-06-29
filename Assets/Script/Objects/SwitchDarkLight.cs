using UnityEngine;
using System.Collections;
using Enums;

public class SwitchDarkLight : MonoBehaviour
{
	void OnCollisionEnter2D(Collision2D collision)
	{
		if(collision.gameObject.tag == "Player")
		{
			Global.ingame.ChangeDarkLight();
		}
	}
}
