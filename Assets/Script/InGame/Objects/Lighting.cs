using UnityEngine;
using UnityEngine.Assertions;
using System.Collections;
using System.Collections.Generic;
using System;
using Enums;

public class Lighting: MonoBehaviour, IRestartable
{
	public int lightTerm;
	public int darkTerm;
	public new Sprite light;
	public Sprite dark;
	public LightingChecker lightingChecker;

	private int length;
	
	GameObject[] effectGameObjects;

	bool isLighting;
	bool isEffectLighting;

	void Start()
	{
		isLighting = false;
		Global.ingame.LightsInMap.Add(this);
		repeat ();
	}

	public HashSet<GameObject> GetGameObjectsInLighting()
	{
		LightingChecker checker = GetComponentInChildren<LightingChecker> ();
		if (checker == null)
		{
			Debug.Log("Light's checker is not exist.");
			return new HashSet<GameObject>();
		}

		if (isLighting)
		{
			return GetComponentInChildren<LightingChecker> ().lightingEffectObjects;
		}
		else
		{
			return new HashSet<GameObject>();
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
		InvokeRepeating ("changeDark", 0, lightTerm + darkTerm);
		InvokeRepeating ("changeLight", darkTerm, lightTerm + darkTerm);
	}

	void IRestartable.Restart()
	{
		Global.ingame.isDark = IsDark.Light;
	}

}
