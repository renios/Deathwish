using UnityEngine;
using UnityEngine.Assertions;
using System.Collections;
using System.Collections.Generic;
using System;
using Enums;

public class Lighting: MonoBehaviour, IRestartable
{
	public int startDelay;
	public int lightTerm;
	public int darkTerm;
	public GameObject lightningEffect;

	public bool isLighting;
	GameObject effect;

	void Start()
	{
		isLighting = false;
		Global.ingame.LightsInMap.Add(this);
		Invoke("repeat", startDelay);
	}

	void repeat()
	{
		InvokeRepeating ("changeDark", 0, lightTerm + darkTerm);
		InvokeRepeating ("changeLight", darkTerm, lightTerm + darkTerm);
	}

	void changeLight()
	{
		isLighting = true;
		SoundEffectController soundEffectController 
			= GameObject.FindObjectOfType(typeof(SoundEffectController)) as SoundEffectController;
		soundEffectController.Play (SoundType.Lightning);
		effect = Instantiate(lightningEffect);
	}

	void changeDark()
	{
		isLighting = false;
	}

	void IRestartable.Restart()
	{
		// It strange that changing global value here.
		Global.ingame.isDark = IsDark.Light;
		Destroy(effect);
	}

}
