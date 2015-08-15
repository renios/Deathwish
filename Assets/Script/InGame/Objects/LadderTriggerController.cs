using UnityEngine;
using System.Collections;
using Enums;

public class LadderTriggerController : MonoBehaviour
{
	void Start ()
	{
		GetComponent<BoxCollider2D> ().isTrigger = true;
	}

	void Update ()
	{
		if (Global.ingame.GetIsDarkInPosition(gameObject) == IsDark.Dark)
		{
			GetComponent<BoxCollider2D> ().isTrigger = false;
		} else
		{
			GetComponent<BoxCollider2D> ().isTrigger = true;
		}
	}
}
