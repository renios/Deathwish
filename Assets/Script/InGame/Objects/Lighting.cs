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
	public new Sprite light;
	public Sprite dark;
	public GameObject lightningEffect;

	public bool isLighting;
	GameObject effect;

	void Start()
	{
		isLighting = false;
		Global.ingame.LightsInMap.Add(this);
		Invoke("repeat", startDelay);
	}

	void changeLight()
	{
		isLighting = true;
		GetComponent<SpriteRenderer> ().sprite = light;
		SoundEffectController soundEffectController 
			= GameObject.FindObjectOfType(typeof(SoundEffectController)) as SoundEffectController;
		soundEffectController.Play (SoundType.Lightning);
		effect = Instantiate(lightningEffect);
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
		Destroy(effect);
	}

}
