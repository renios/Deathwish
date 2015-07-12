using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Enums;

public class Player : MonoBehaviour, IRestartable
{
	public float moveSpeed;
	public float climbSpeed;
	public float jumpPower;
	private CharacterLocation characterLocation;
	private Vector3 startPoint;
	private Collider2D ladderCollider;
	new private Rigidbody2D rigidbody2D;

	void Start ()
	{
		ladderCollider = null;
		startPoint = gameObject.transform.position;
		rigidbody2D = gameObject.GetComponent<Rigidbody2D> ();
	}

	void Update ()
	{
		if(characterLocation != CharacterLocation.OnLadder)
		{
			Move ();

			if (characterLocation == CharacterLocation.OnBlock && Input.GetKeyDown (KeyCode.Space))
			{
				Jump ();
			}
		}
		else
		{
			Climb ();
		}

		//Restart by R Key is not needed. It should be replaced by restart button in menu.
		if(gameObject.transform.position.y <= -10 || Input.GetKeyDown(KeyCode.R))
		{
			Restarter.RestartAll();
		}
	}

	void OnTriggerStay2D (Collider2D coll)
	{
		StartClimbingLadder (coll);
	}

	void OnTriggerExit2D (Collider2D coll)
	{
		if(characterLocation == CharacterLocation.OnLadder)
		{
			Debug.Assert(coll == ladderCollider, "Ladder collider should be same with coll");
			EscapeFromLadder (ladderCollider);
			characterLocation = CharacterLocation.OnAir;
		}
	}

	void OnCollisionEnter2D (Collision2D collision)
	{
		if(collision.gameObject.tag == "Ground")
		{
			if(characterLocation != CharacterLocation.OnLadder)
			{
				characterLocation = CharacterLocation.OnBlock;
			}
			else
			{
				EscapeFromLadder(ladderCollider);
				characterLocation = CharacterLocation.OnBlock;
			}
		}

		if(collision.gameObject.tag == "Fire" && Global.ingame.isDark == IsDark.Light)
		{
			Restarter.RestartAll();
		}
	}

	void EscapeFromLadder(Collider2D coll)
	{
		if(characterLocation == CharacterLocation.OnLadder)
		{
			rigidbody2D.gravityScale = 1;
			coll.gameObject.GetComponent<CeilingColliderController>().EnableCeiling();
			SetDefaultConstraints();
			ladderCollider = null;
		}
	}

	void StartClimbingLadder(Collider2D coll)
	{
		if((Input.GetKey(KeyCode.UpArrow) || Input.GetKey(KeyCode.DownArrow)) && characterLocation != CharacterLocation.OnLadder)
		{
			characterLocation = CharacterLocation.OnLadder;
			rigidbody2D.gravityScale = 0;
			coll.gameObject.GetComponent<CeilingColliderController>().DisableCeiling();
			SetPositionXAtCenterOfLadder(coll);
			SetConstraintsforLadder();
			ladderCollider = coll;
		}
	}

	void Move ()
	{
		float horizontalSpeed = Input.GetAxis ("Horizontal") * moveSpeed * Time.deltaTime;
		rigidbody2D.AddForce (new Vector2 (horizontalSpeed, 0));
	}

	void Jump ()
	{
		rigidbody2D.AddForce (new Vector2 (0, jumpPower));
		characterLocation = CharacterLocation.OnAir;
	}

	void Climb ()
	{
		float verticalSpeed = Input.GetAxis ("Vertical") * climbSpeed * Time.deltaTime;
		gameObject.transform.Translate(new Vector2 (0, verticalSpeed));
	}

	void IRestartable.Restart()
	{
		rigidbody2D.isKinematic = true;
		gameObject.transform.position = startPoint;
		rigidbody2D.isKinematic = false;
		SetDefaultConstraints ();
		characterLocation = CharacterLocation.OnAir;
		ladderCollider = null;
		//Temporarily reset isDark in Player.cs, but it should be moved to other script.
		Global.ingame.isDark = IsDark.Light;
	}

	void SetPositionXAtCenterOfLadder(Collider2D coll)
	{
		float ladderX = coll.gameObject.transform.position.x;
		Vector3 playerPosition = gameObject.transform.position;
		playerPosition.x = ladderX;
		gameObject.transform.position = playerPosition;
	}

	//This is necessary since character may tumble when colliding.
	void SetDefaultConstraints()
	{
		rigidbody2D.constraints = RigidbodyConstraints2D.FreezeRotation;
	}

	//FreezePositionX is chosen since FreezeRotation and FreezePositionX can't be chosen at the same time.
	void SetConstraintsforLadder()
	{
		rigidbody2D.constraints = RigidbodyConstraints2D.FreezePositionX;
	}
}