using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class LevelSelectManager : MonoBehaviour
{

	public string[] LevelTags;

	public GameObject[] Locks;
	public bool[] LevelUnlocked;

	public Button[] Interactable;

	void Start()
	{
		for (int i = 0; i < LevelTags.Length; i++)
		{
			if (PlayerPrefs.GetInt(LevelTags[i]) == 0)
			{
				LevelUnlocked[i] = false;
			}
			else
			{
				LevelUnlocked[i] = true;
			}

			if (LevelUnlocked[i])
			{
				Locks[i].SetActive(false);
				Interactable[i].interactable = true;
			}
		}
	}

	void Update()
	{
		
	}

}
