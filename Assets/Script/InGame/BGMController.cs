using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class BGMController : MonoBehaviour
{
	AudioSource audioSource;
	public AudioClip IntroSound;
	public AudioClip CutSceneSound;
	public AudioClip Chapter1Sound;
	public AudioClip Chapter2Sound;
	List<AudioClip> clips = new List<AudioClip>();

	void Awake()
	{
		audioSource = GetComponent<AudioSource> ();
		SetBGM ();
		clips.Add (Chapter1Sound);
		clips.Add (Chapter2Sound);
		clips.Add (IntroSound);
	}

	//Implementation for cutscene bgm needed
	void SetBGM()
	{
		if(Scene.currentSceneType == Scene.SceneType.MainScene || Scene.currentSceneType == Scene.SceneType.StageSelect)
		{
			audioSource.clip = IntroSound;
		}

		if(Scene.currentSceneType == Scene.SceneType.Stage)
		{
			switch(Scene.currentLevelTag.Chapter)
			{
				case 0:
					audioSource.clip = IntroSound;
					break;
				case 1:
					audioSource.clip = Chapter1Sound;
					break;
				case 2:
					audioSource.clip = Chapter2Sound;
					break;
				case 3:
					audioSource.clip = Chapter1Sound;
					break;
				case 4:
					audioSource.clip = IntroSound;
					break;
				case 5:
					audioSource.clip = Chapter1Sound;
					break;
				default:
					//need another case
					audioSource.clip = clips[Random.Range(0, clips.Count - 1)];
					return;
			}
		}

		return;
	}
}
