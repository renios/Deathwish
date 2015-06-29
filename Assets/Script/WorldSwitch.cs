using UnityEngine;
using System.Collections;
using Enums;

public class WorldSwitch : MonoBehaviour
{
	void OnCollisionEnter2D(Collision2D collision)
	{
		if(collision.gameObject.tag == "Player")
		{
			Global.world.WorldChange();
		}
	}
}
