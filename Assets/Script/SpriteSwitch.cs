using UnityEngine;
using System.Collections;
using Enums;

public class SpriteSwitch : MonoBehaviour
{

	public Sprite Light;
	public Sprite Dark;
	public InGame controller;

	private SpriteRenderer sr;

	void Start ()
	{
		sr = GetComponent<SpriteRenderer> ();
	}

	void Update ()
	{
		if(Global.ingame.isdark == isDark.Light)
		{
			sr.sprite = Light;
		}
		else if(Global.ingame.isdark == isDark.Dark)
		{
			sr.sprite = Dark;
		}
	}
}
