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
			coll.gameObject.GetComponent<ColliderController>().EnableCeiling();
			SetDefaultConstraints();
			ladderCollider = null;
		}
	}

	void StartClimbingLadder(Collider2D coll)
	{
		if((Input.GetKeyDown(KeyCode.UpArrow) || Input.GetKeyDown(KeyCode.DownArrow)) && characterLocation != CharacterLocation.OnLadder)
		{
			characterLocation = CharacterLocation.OnLadder;
			rigidbody2D.gravityScale = 0;
			coll.gameObject.GetComponent<ColliderController>().DisableCeiling();
			SetPositionAtCenterOfLadder(coll);
			SetConstraintsforLadder();
			ladderCollider = coll;
		}
	}

	void Move ()
	{
		float speed1 = Input.GetAxis ("Horizontal") * moveSpeed * Time.deltaTime;
		rigidbody2D.AddForce (new Vector2 (speed1, 0));
	}

	void Jump ()
	{
		rigidbody2D.AddForce (new Vector2 (0, jumpPower));
		characterLocation = CharacterLocation.OnAir;
	}

	void Climb ()
	{
		float speed2 = Input.GetAxis ("Vertical") * climbSpeed * Time.deltaTime;
		gameObject.transform.Translate(new Vector2 (0, speed2));
	}

	void IRestartable.Restart()
	{
		rigidbody2D.isKinematic = true;
		gameObject.transform.position = startPoint;
		rigidbody2D.isKinematic = false;
		SetDefaultConstraints ();
		characterLocation = CharacterLocation.OnAir;
		ladderCollider = null;
	}

	void SetPositionAtCenterOfLadder(Collider2D coll)
	{
		float a = coll.gameObject.transform.position.x;
		Vector3 b = gameObject.transform.position;
		b.x = a;
		gameObject.transform.position = b;
	}

	void SetDefaultConstraints()
	{
		rigidbody2D.constraints = RigidbodyConstraints2D.FreezeRotation;
	}

	void SetConstraintsforLadder()
	{
		rigidbody2D.constraints = RigidbodyConstraints2D.FreezePositionX;
	}
}