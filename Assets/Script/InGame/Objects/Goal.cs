using UnityEngine;
using System.Collections;

public class Goal : MonoBehaviour, IRestartable
{
	private bool playerInZone;

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
			SoundEffectController soundEffectController
				= GameObject.FindObjectOfType(typeof(SoundEffectController)) as SoundEffectController;
			soundEffectController.Play(Enums.SoundType.OpenDoor);
			LoadLevel();
		}
	}

	public void LoadLevel()
	{
		Scene.LoadNextStageAndSave();
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
