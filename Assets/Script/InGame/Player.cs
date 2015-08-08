using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Enums;

public class Player : MonoBehaviour, IRestartable
{
	public float moveSpeed;
	public float jumpPower;
	public float climbSpeed;

	private Vector3 startPoint;
	private float yOfLowestObject;

	float gravityScaleOfStartTime;

	public GroundChecker groundChecker;
	public LadderChecker ladderChecker;
	private Climber climber;

	public GameObject playerSpriteObject;

	void Start ()
	{
		startPoint = gameObject.transform.position;
		gravityScaleOfStartTime = GetComponent<Rigidbody2D> ().gravityScale;
		yOfLowestObject = LowestObjectFinder.Find ().position.y;
		climber = new Climber (gameObject, ladderChecker, groundChecker, climbSpeed);
	}
	
	void Update ()
	{
		if(gameObject.transform.position.y - yOfLowestObject <= -10 || Input.GetKeyDown(KeyCode.R))
		{
			Restarter.RestartAll();
		}

		Move ();
		Jump ();
		climber.Update ();
	}
	
	void Move ()
	{
		if (Input.GetKey (KeyCode.RightArrow))
		{
			GetComponent<Rigidbody2D>().velocity = new Vector2(moveSpeed, GetComponent<Rigidbody2D>().velocity.y);
			playerSpriteObject.transform.rotation = Quaternion.Euler(0, 180, 0);
		}
		else if (Input.GetKey(KeyCode.LeftArrow))
		{
			GetComponent<Rigidbody2D>().velocity = new Vector2(-moveSpeed, GetComponent<Rigidbody2D>().velocity.y);
			playerSpriteObject.transform.rotation = Quaternion.Euler(0, 0, 0);
		}
		else
		{
			GetComponent<Rigidbody2D>().velocity = new Vector2(0, GetComponent<Rigidbody2D>().velocity.y);
		}
	}

	private void Jump()
	{
		if (Input.GetKeyDown (KeyCode.Space) && groundChecker.IsGrounded())
		{
			GetComponent<Rigidbody2D>().velocity = new Vector2(GetComponent<Rigidbody2D>().velocity.x, jumpPower);
		}
	}

	void IRestartable.Restart()
	{
		GetComponent<Rigidbody2D>().velocity = Vector2.zero;
		gameObject.transform.position = startPoint;
		//Temporarily reset isDark in Player.cs, but it should be moved to other script.
		Global.ingame.isDark = IsDark.Light;
		climber = new Climber (gameObject, ladderChecker, groundChecker, climbSpeed);
		GetComponent<Rigidbody2D> ().gravityScale = gravityScaleOfStartTime;
	}
}