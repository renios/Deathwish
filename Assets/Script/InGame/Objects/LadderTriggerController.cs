using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Enums;

public class LadderTriggerController : MonoBehaviour, IRestartable
{
	//Some changes needed for chapter 5.
	//In Chapter 5, lighting with ladder makes problem.
	public bool penetrate;
	new Player player;

	void Start ()
	{
		penetrate = false;
		GetComponent<BoxCollider2D> ().isTrigger = true;
		player = GameObject.FindObjectOfType (typeof(Player)) as Player;
	}

	void Update ()
	{
		foreach(Lighting lighting in Global.ingame.LightsInMap)
		{
			if(lighting.isLighting) penetrate = true;
		}

		if(!player.ladderCheckerUp.isUpLaddered && !player.ladderCheckerDown.isDownLaddered)
		{
			penetrate = false;
		}

		if(Global.ingame.GetIsDarkInPosition(gameObject) == IsDark.Dark && !penetrate)
		{
			GetComponent<BoxCollider2D> ().isTrigger = false;
			return;
		}

		GetComponent<BoxCollider2D> ().isTrigger = true;
	}

	void IRestartable.Restart()
	{
		penetrate = false;
	}
}
