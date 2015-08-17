using UnityEngine;
using System.Collections;
using Enums;

public class Water : MonoBehaviour, IRestartable {

	bool isActive;

	public bool IsActive()
	{
		return isActive;
	}

	// Update is called once per frame
	void Update () {
		if (Global.ingame.isDark == IsDark.Light)
		{
			GetComponent<SpriteRenderer>().enabled = true;
			isActive = true;
		}
		else if (Global.ingame.isDark == IsDark.Dark)
		{
			GetComponent<SpriteRenderer>().enabled = true;
			isActive = false;
		}
	}
	
	void IRestartable.Restart()
	{
		GetComponent<SpriteRenderer>().enabled = true;
		isActive = true;
	}
}
