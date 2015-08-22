using UnityEngine;
using System.Collections;
using Enums;

public class Wind : MonoBehaviour
{
	public WindDirection windDirection;

	void OnTriggerEnter2D(Collider2D other)
	{
		if (other.gameObject.tag == "Player")
		{
			Global.ingame.inWind = true;
		}
	}

	void OnTriggerExit2D(Collider2D other)
	{
		if (other.gameObject.tag == "Player")
		{
			Global.ingame.inWind = false;
		}
	}

}
