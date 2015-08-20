using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Enums;

public class SoundEffectController : MonoBehaviour
{
	public Player player;
	public AudioSource audioSource;
	public List<AudioClip> audioClips;
	public CharacterAction characterAction;
	public float delay;
	float timeAfterPlay;
	bool recentlyPlayed;

	void Start()
	{
		audioSource = GetComponent<AudioSource> ();
		characterAction = CharacterAction.Default;
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

	public void Play()
	{
		if(characterAction == CharacterAction.Walk)
		{
			if(recentlyPlayed) return;

			audioSource.PlayOneShot(audioClips[Random.Range(2, 4)]);
			timeAfterPlay = 0;
			recentlyPlayed = true;
			return;
		}

		timeAfterPlay = 0;
		recentlyPlayed = false;

		if(characterAction == CharacterAction.Default) return;
		if(characterAction == CharacterAction.Jump) audioSource.PlayOneShot(audioClips[0]);
		if(characterAction == CharacterAction.Land) audioSource.PlayOneShot(audioClips[1]);
	}
}
