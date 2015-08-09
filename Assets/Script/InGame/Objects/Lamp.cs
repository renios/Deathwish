using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Enums;

public class Lamp : ObjectMonoBehaviour
{
	public LampProperty lampProperty;
	public float detectingRadius;
	public GameObject LightParticleObject;
	public GameObject DarkParticleObject;
	private ParticleSystem particle;
	//forDebugging
	List<Lamp> lamps;

	void Start()
	{
		Global.ingame.LampsInMap.Add (this);
		lamps = new List<Lamp>(Global.ingame.LampsInMap);
		
		if (lampProperty == LampProperty.LightLamp)
		{
			particle = LightParticleObject.GetComponent<ParticleSystem>();
			DarkParticleObject.SetActive(false);
		}
		else if (lampProperty == LampProperty.DarkLamp)
		{
			particle = DarkParticleObject.GetComponent<ParticleSystem>();
			LightParticleObject.SetActive(false);
		}
		particle.startSize *= detectingRadius;
	}

	public override void UpdateByParent ()
	{
		GetComponent<SpriteRenderer> ().enabled = true;
		GetComponent<Collider2D> ().enabled = true;
	}
	
	public override void HideObject()
	{
		return;
	}
}
