using UnityEngine;
using System.Collections;
using Enums;

public class Water : MonoBehaviour, IRestartable {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		if (Global.ingame.isDark == IsDark.Light)
		{
			GetComponent<SpriteRenderer>().enabled = true;
			tag = "Water";
		}
		else if (Global.ingame.isDark == IsDark.Dark)
		{
			GetComponent<SpriteRenderer>().enabled = true;
			tag = "Untagged";
		}
	}
	
	void IRestartable.Restart()
	{
		GetComponent<SpriteRenderer>().enabled = true;
		tag = "Water";
	}
}
