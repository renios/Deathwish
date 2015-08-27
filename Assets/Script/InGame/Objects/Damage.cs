using UnityEngine;
using System.Collections;
using Enums;

public class Damage : MonoBehaviour
{
	public bool isActiveAtLight = false;
	public bool isActiveAtDark = false;

	void OnCollisionEnter2D (Collision2D collision)
	{
		if (collision.gameObject.tag == "Player")
		{
			IsDark isDarkNow = Global.ingame.GetIsDarkInPosition(gameObject);
			if ((isActiveAtLight && (isDarkNow == IsDark.Light)) ||
				(isActiveAtDark && (isDarkNow == IsDark.Dark)))
			GameObject.FindObjectOfType<Player>().PlayDieAnimSoundAndRestart(soundType);
		}
	}

	void OnTriggerEnter2D (Collider2D collision)
	{
		if (collision.gameObject.tag == "Player")
		{
			IsDark isDarkNow = Global.ingame.GetIsDarkInPosition(gameObject);
			if ((isActiveAtLight && (isDarkNow == IsDark.Light)) ||
				(isActiveAtDark && (isDarkNow == IsDark.Dark)))
			GameObject.FindObjectOfType<Player>().PlayDieAnimSoundAndRestart(soundType);
		}
	}

	SoundType soundType
	{
		get
		{
			if(this.tag == "Spike") return SoundType.SpikeDeath;
			if(this.tag == "Fire") return SoundType.FireDeath;
			if(this.tag == "Grass") return SoundType.SpikeDeath;
			//AreaMarker has Damage and Dust doesn't.
			if(this.tag == "AreaMarker") return SoundType.DustDeath;
			else return SoundType.None;
		}
	}
}