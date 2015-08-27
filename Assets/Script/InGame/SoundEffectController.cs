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
	float timeAfterPlay;
	bool recentlyPlayed;

	void Start()
	{
		audioSource = GetComponent<AudioSource> ();
		recentlyPlayed = false;
	}

	void Update()
	{
		if(recentlyPlayed)
		{
			timeAfterPlay += Time.deltaTime;
		}

		if(timeAfterPlay > delay)
		{
			recentlyPlayed = false;
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
		   || soundType == SoundType.BoxPush || soundType == SoundType.Swim)
		{
			if(recentlyPlayed) return;

			timeAfterPlay = 0;
			recentlyPlayed = true;

			switch(soundType)
			{
				case SoundType.BoxPush:
					audioSource.PlayOneShot(boxPushSound);
					break;
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

			return;
		}

		timeAfterPlay = 0;
		recentlyPlayed = false;

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
		timeAfterPlay = 0;
		recentlyPlayed = false;
	}
}
