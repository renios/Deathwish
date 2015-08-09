using UnityEngine;
using UnityEngine.Assertions;
using System.Collections;
using System;
using Enums;

public class Lighting: MonoBehaviour, IRestartable
{
	public new Sprite light;
	public Sprite dark;
	public LightingChecker lightingChecker;

	private int length;
	
	GameObject[] effectGameObjects;

	bool isLighting;
	bool isEffectLighting;

	void Start()
	{
		effectGameObjects = GetComponentInChildren<LightingChecker> ().lightingEffectObjects;
		isLighting = false;
		length = effectGameObjects.Length;
		repeat ();
	}

	void Update()
	{
		if (lightingChecker.IsEffectLighting () && Global.ingame.isDark == IsDark.Dark && isLighting == true)
		{
			for(int i = 0; i < length; i++)
			{
				if (effectGameObjects[i].GetComponent<SpriteSwitch>() != null)
					effectGameObjects[i].GetComponent<SpriteSwitch>().isDark = IsDark.Light;
			}
		}
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
