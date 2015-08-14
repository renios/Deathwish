using UnityEngine;
using System.Collections;
using Enums;

public class SpriteSwitch : MonoBehaviour
{
	public new Sprite light;
	public Sprite dark;

	private SpriteRenderer sr;

	void Start ()
	{
		sr = GetComponent<SpriteRenderer> ();
	}

	//fatal problem. Need to solve.
	void Update ()
	{
		IsDark isDarkNow = Global.ingame.GetIsDarkInPosition(gameObject);
		if(isDarkNow == IsDark.Light)
		{
			sr.sprite = light;
		}
		else if(isDarkNow == IsDark.Dark)
		{
			sr.sprite = dark;
		}
	}
}
