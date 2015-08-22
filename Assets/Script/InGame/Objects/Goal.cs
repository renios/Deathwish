using UnityEngine;
using System.Collections;

public class Goal : MonoBehaviour, IRestartable
{
	private bool playerInZone;

	public string LevelToLoad;

	public string LevelTag;
	
	private bool isGround;

	void Start()
	{
		playerInZone = false;
	}

	void Update()
	{
		isGround = GameObject.FindObjectOfType<GroundChecker> ().IsGrounded ();
		if (Input.GetKeyDown(KeyCode.UpArrow) && playerInZone && isGround)
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
