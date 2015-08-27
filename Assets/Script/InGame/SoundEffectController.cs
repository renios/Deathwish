using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Enums;

public class SoundEffectController : MonoBehaviour, IRestartable
{
	public AudioClip walkSound;
	public AudioClip landSound;
	public AudioClip JumpSound;
	public AudioClip grassPassingSound;
	public AudioClip swimSound;
	public AudioClip boxFallingSound;
	public AudioClip boxPushSound;
	public AudioClip openDoorSound;
	public AudioClip lightningSound;
	public AudioClip decaySound;
	public AudioClip mirrorSound;
	public AudioClip fireDeathSound;
	public AudioClip spikeDeathSound;
	public AudioClip dustDeathSound;
	public AudioClip fireIsCloseSound;

	public AudioSource audioSource;
	public float delay;
	public Dictionary<string, float> dicTimeAfterPlay = new Dictionary<string, float>();
	public Dictionary<string, bool> dicRecentlyPlayed = new Dictionary<string, bool>();
	float timeAfterPlay;
	bool recentlyPlayed;

	void Start()
	{
		audioSource = GetComponent<AudioSource> ();
		dicTimeAfterPlay.Add ("Move", 0);
		dicTimeAfterPlay.Add ("Push", 0);
		dicRecentlyPlayed.Add ("Move", false);
		dicRecentlyPlayed.Add ("Push", false);
	}

	void Update()
	{
		var keys1 = new List<string>(dicTimeAfterPlay.Keys);
		foreach(string key in keys1)
		{
			if(dicRecentlyPlayed[key]) dicTimeAfterPlay[key] += Time.deltaTime;
		}

		var keys2 = new List<string>(dicRecentlyPlayed.Keys);
		foreach(string key in keys2)
		{
			if(dicTimeAfterPlay[key] > delay) dicRecentlyPlayed[key] = false;
		}
	}

	public void Play(SoundType soundType)
	{
		if(soundType == SoundType.FireDeath || soundType == SoundType.DustDeath
		   || soundType == SoundType.SpikeDeath)
		{
			switch(soundType)
			{
				case SoundType.DustDeath:
					audioSource.PlayOneShot(dustDeathSound);
					break;
				case SoundType.FireDeath:
					audioSource.PlayOneShot(fireDeathSound);
					break;
				case SoundType.SpikeDeath:
					audioSource.PlayOneShot(spikeDeathSound);
					break;
				default:
					break;
			}

			return;
		}

		if(soundType == SoundType.Walk || soundType == SoundType.GrassPassing
		   || soundType == SoundType.Swim)
		{
			if (dicRecentlyPlayed ["Move"]){}
			else
			{
				dicTimeAfterPlay["Move"] = 0;
				dicRecentlyPlayed["Move"] = true;
				
				switch(soundType)
				{
				case SoundType.Walk:
					audioSource.PlayOneShot(walkSound);
					break;
				case SoundType.GrassPassing:
					audioSource.PlayOneShot(grassPassingSound);
					break;
				case SoundType.Swim:
					audioSource.PlayOneShot(swimSound);
					break;
				default:
					break;
				}
			}
		}
		else
		{
			dicTimeAfterPlay["Move"] = 0;
			dicRecentlyPlayed["Move"] = false;
		}

		//It Should use another variables.
		if(soundType == SoundType.BoxPush)
		{
			if(dicRecentlyPlayed["Push"]){}
			else
			{
				dicTimeAfterPlay["Push"] = 0;
				dicRecentlyPlayed["Push"] = true;
				//audioSource.PlayOneShot(boxPushSound);
			}
		}
		else
		{
			dicTimeAfterPlay["Push"] = 0;
			dicRecentlyPlayed["Push"] = false;
		}

		if(soundType == SoundType.None) return;
		if(soundType == SoundType.Jump) audioSource.PlayOneShot(JumpSound);
		if(soundType == SoundType.Land) audioSource.PlayOneShot(landSound);

		if(soundType == SoundType.OpenDoor) audioSource.PlayOneShot(openDoorSound);
		if(soundType == SoundType.Mirror) audioSource.PlayOneShot(mirrorSound);

		if(soundType == SoundType.Decay) audioSource.PlayOneShot(decaySound);
		if(soundType == SoundType.FireIsClose) audioSource.PlayOneShot(fireIsCloseSound);
		if(soundType == SoundType.Lightning) audioSource.PlayOneShot(lightningSound);

		if(soundType == SoundType.BoxFalling) audioSource.PlayOneShot(boxFallingSound);

		return;
	}

	void IRestartable.Restart()
	{
		var keys1 = new List<string>(dicTimeAfterPlay.Keys);
		foreach(var key in keys1)
		{
			dicTimeAfterPlay[key] = 0;
		}
		
		var keys2 = new List<string>(dicRecentlyPlayed.Keys);
		foreach(var key in keys2)
		{
			dicRecentlyPlayed[key] = false;
		}
	}
}
