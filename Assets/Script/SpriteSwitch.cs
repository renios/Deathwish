using UnityEngine;
using System.Collections;
using Enums;

public class SpriteSwitch : MonoBehaviour
{

	public Sprite Light;
	public Sprite Dark;
	public WorldControl controller;

	private SpriteRenderer sr;

	void Start ()
	{
		sr = GetComponent<SpriteRenderer> ();
	}

	void Update ()
	{
		if(controller.World == isDark.Light)
		{
			sr.sprite = Light;
		}
		else if(controller.World == isDark.Dark)
		{
			sr.sprite = Dark;
		}
	}
}
