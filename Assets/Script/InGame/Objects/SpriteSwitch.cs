using UnityEngine;
using System.Collections;
using Enums;

public class SpriteSwitch : MonoBehaviour
{
	public IsDark isDark;

	public new Sprite light;
	public Sprite dark;

	private SpriteRenderer sr;

	void Start ()
	{
		sr = GetComponent<SpriteRenderer> ();
	}

	void Update ()
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
}
