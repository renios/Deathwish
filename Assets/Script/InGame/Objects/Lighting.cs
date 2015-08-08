using UnityEngine;
using UnityEngine.Assertions;
using System.Collections;
using System;
using Enums;

public class Lighting: MonoBehaviour, IRestartable
{
	public new Sprite light;
	public Sprite dark;

	bool isLighting;

	void Start()
	{
		isLighting = false;
		repeat ();
	}

	void changeLight()
	{
		isLighting = true;
		GetComponent<SpriteRenderer> ().sprite = light;
	}
	void changeDark()
	{
		isLighting = false;
		GetComponent<SpriteRenderer> ().sprite = dark;
	}

	void repeat()
	{
		InvokeRepeating ("changeDark", 0, 10);
		InvokeRepeating ("changeLight", 5, 10);
	}

	void IRestartable.Restart()
	{
		Global.ingame.isDark = IsDark.Light;
	}

}
