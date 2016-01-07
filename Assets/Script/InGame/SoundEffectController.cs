using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Enums;
using System;

public class SoundEffectController : MonoBehaviour, IRestartable
{
	public AudioClip walkSound;
	public AudioClip landSound;
	public AudioClip JumpSound;
	public AudioClip grassPassingSound;
	public AudioClip swimSound;
	public AudioClip ladderClimbingSound;
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
	//public AudioClip windSound;

	public AudioSource audioSource;
	public float moveSoundDelay;
	public float pushSoundDelay;
	public float fireSoundDelay;
	//public float windSoundDelay;
	public Dictionary<string, float> dicSoundDelay = new Dictionary<string, float>();
	public Dictionary<string, float> dicTimeAfterPlay = new Dictionary<string, float>();
	public Dictionary<string, bool> dicRecentlyPlayed = new Dictionary<string, bool>();

	private SoundContainer moveSoundContainer = new SoundContainer();
	private SoundContainer pushSoundContainer = new SoundContainer();
	private SoundContainer fireSoundContainer =new SoundContainer();
	private bool boxFallenSoundPlayedThisFrame = false;

	void Start()
	{
		moveSoundContainer.Start("MoveSoundSource");
		pushSoundContainer.Start("PushSoundContainer");
		fireSoundContainer.Start("FireSoundContainer");

		audioSource = GetComponent<AudioSource> ();

		dicTimeAfterPlay.Add ("Move", 0);
		dicRecentlyPlayed.Add ("Move", false);
		dicSoundDelay.Add ("Move", moveSoundDelay);

		dicTimeAfterPlay.Add ("Push", 0);
		dicRecentlyPlayed.Add ("Push", false);
		dicSoundDelay.Add ("Push", pushSoundDelay);

		dicTimeAfterPlay.Add ("Fire", 0);
		dicRecentlyPlayed.Add ("Fire", false);
		dicSoundDelay.Add ("Fire", fireSoundDelay);

		/*dicTimeAfterPlay.Add ("Wind", 0);
		dicRecentlyPlayed.Add ("Wind", false);
		dicSoundDelay.Add ("Wind", windSoundDelay);*/
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
			if(dicTimeAfterPlay[key] > dicSoundDelay[key]) dicRecentlyPlayed[key] = false;
		}
		
		moveSoundContainer.Update();
		pushSoundContainer.Update();
		fireSoundContainer.Update();
		boxFallenSoundPlayedThisFrame = false;
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

		PlayMoveSound(soundType);

		if(soundType == SoundType.BoxPush)
		{
			pushSoundContainer.ChangeVolume(0.4f);
			pushSoundContainer.Play(boxPushSound);
		}

		if(soundType == SoundType.FireIsClose)
		{
			fireSoundContainer.Play(fireIsCloseSound);
		}

		/*if(soundType == SoundType.WindIsClose)
		{
			if(dicRecentlyPlayed["Wind"]){}
			else
			{
				//Debug.Log("Also Pass at " + t.ToString("F4") + " with " + dicRecentlyPlayed["Wind"].ToString());
				dicTimeAfterPlay["Wind"] = 0;
				dicRecentlyPlayed["Wind"] = true;
				audioSource.PlayOneShot(windSound);
			}
		}
		else
		{
			dicTimeAfterPlay["Wind"] = 0;
			dicRecentlyPlayed["Wind"] = false;
		}*/

		if(soundType == SoundType.None) return;
		if(soundType == SoundType.Jump) audioSource.PlayOneShot(JumpSound);
		if(soundType == SoundType.Land) audioSource.PlayOneShot(landSound);

		if(soundType == SoundType.OpenDoor) audioSource.PlayOneShot(openDoorSound);
		if(soundType == SoundType.Mirror) audioSource.PlayOneShot(mirrorSound);

		if(soundType == SoundType.Decay) audioSource.PlayOneShot(decaySound);
		if(soundType == SoundType.Lightning) audioSource.PlayOneShot(lightningSound);

		if(soundType == SoundType.BoxFalling && boxFallenSoundPlayedThisFrame == false) {
			audioSource.PlayOneShot(boxFallingSound, 0.2f);
			boxFallenSoundPlayedThisFrame = true;
		}

		return;
	}
	
	public class SoundContainer
	{
		private AudioSource container;
		public void Start(string name)
		{
			var gameObject = new GameObject(name);
			gameObject.AddComponent<AudioSource>();
			container = gameObject.GetComponent<AudioSource>();
		}
		
		private float previousSoundTime;
		
		public void ChangeVolume(float volume)
		{
			container.volume = volume;
		}
		
		public void Play(AudioClip clip)
		{
			if (!container.isPlaying)
			{
				container.clip = clip;
				container.Play();
			}
			previousSoundTime = Time.time;
		}
		
		public void Update()
		{
			if (previousSoundTime < Time.time - (Time.deltaTime * 2))
			{
				container.Stop();
			}
		}
	}

    private void PlayMoveSound(SoundType soundType)
    {
		if(soundType == SoundType.Walk || soundType == SoundType.GrassPassing
		   || soundType == SoundType.Swim || soundType == SoundType.ClimbingLadder)
		{
			dicTimeAfterPlay["Move"] = 0;
			dicRecentlyPlayed["Move"] = true;
			
			switch(soundType)
			{
				case SoundType.Walk:
					moveSoundContainer.Play(walkSound);
					break;
				case SoundType.GrassPassing:
					moveSoundContainer.Play(grassPassingSound);
					break;
				case SoundType.Swim:
					moveSoundContainer.Play(swimSound);
					break;
				case SoundType.ClimbingLadder:
					moveSoundContainer.Play(ladderClimbingSound);
					break;
				default:
					break;
			}
		}
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
