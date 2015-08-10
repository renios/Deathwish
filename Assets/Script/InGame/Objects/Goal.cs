using UnityEngine;
using System.Collections;

public class Goal : MonoBehaviour, IRestartable
{
	private bool playerInZone;

	public string LevelToLoad;

	public string LevelTag;

	void Start()
	{
		playerInZone = false;
	}

	void Update()
	{
		if (Input.GetKeyDown(KeyCode.UpArrow) && playerInZone)
		{
			LoadLevel();
		}
	}

	public void LoadLevel()
	{
		PlayerPrefs.SetInt(LevelTag, 1);
		Scene.Load(LevelToLoad, Scene.SceneType.Stage);

	}
	void OnTriggerEnter2D(Collider2D collision)
	{
		if(collision.gameObject.tag == "Player")
		{
			playerInZone = true;
		}
	}

	void OnTriggerExit2D(Collider2D collision)
	{
		if (collision.gameObject.tag == "Player")
		{
			playerInZone = false;
		}
	}

	void IRestartable.Restart()
	{
		playerInZone = false;
	}
}
