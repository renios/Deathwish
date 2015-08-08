using UnityEngine;
using System.Collections;
using Enums;

public class SpriteSwitch : MonoBehaviour
{
	public IsDark isDark;
	public bool changed;

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
		if(changed)
		{
			if(isDark == IsDark.Light)
			{
				sr.sprite = light;
			}
			else if(isDark == IsDark.Dark)
			{
				sr.sprite = dark;
			}
		}
		else
		{
			if(Global.ingame.isDark == IsDark.Light)
			{
				sr.sprite = light;
			}
			else if(Global.ingame.isDark == IsDark.Dark)
			{
				sr.sprite = dark;
			}
		}
	}
}
