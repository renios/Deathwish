using UnityEngine;
using System.Collections;
using Enums;

public class Damage : MonoBehaviour
{
	public bool isActiveAtLight = false;
	public bool isActiveAtDark = false;

	void OnCollisionEnter2D (Collision2D collision)
	{
		if (collision.gameObject.tag == "Player")
		{
			IsDark isDarkNow = Global.ingame.GetIsDarkInPosition(transform.position);
			if ((isActiveAtLight && (isDarkNow == IsDark.Light)) ||
				(isActiveAtDark && (isDarkNow == IsDark.Dark)))
			Restarter.RestartAll();
		}
	}

	void OnTriggerEnter2D (Collider2D collision)
	{
		if (collision.gameObject.tag == "Player")
		{
			IsDark isDarkNow = Global.ingame.GetIsDarkInPosition(transform.position);
			if ((isActiveAtLight && (isDarkNow == IsDark.Light)) ||
				(isActiveAtDark && (isDarkNow == IsDark.Dark)))
			Restarter.RestartAll();
		}
	}
}