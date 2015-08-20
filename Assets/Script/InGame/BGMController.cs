using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class BGMController : MonoBehaviour
{
	AudioSource audioSource;
	public List<AudioClip> BGMList;

	void Awake()
	{
		audioSource = GetComponent<AudioSource> ();
		SetBGM ();
	}

	void SetBGM()
	{
		if(Scene.currentSceneType == Scene.SceneType.MainScene)
		{
			audioSource.clip = BGMList[0];
		}
		if(Scene.currentSceneType == Scene.SceneType.StageSelect)
		{
			audioSource.clip = BGMList[0];
			//need another clip
		}
		if(Scene.currentSceneType == Scene.SceneType.Stage)
		{
			audioSource.clip = BGMList[0];
			//need another clip
		}

		return;
	}
}
