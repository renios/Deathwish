using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Enums;

public class Player : MonoBehaviour, IRestartable
{
	public float moveSpeed;
	public float jumpPower;
	public float climbSpeed;
	public float maxSpeedInWater;

	private Vector3 startPoint;
	private float yOfLowestObject;

	float gravityScaleOfStartTime;

	public GroundChecker groundChecker;
	public LadderCheckerUp ladderCheckerUp;
	public LadderCheckerDown ladderCheckerDown;
	private Climber climber;

	public GameObject playerSpriteObject;
	private Animator animator;

	private O2Checker O2Checker;

	void Start ()
	{
		O2Checker = FindObjectOfType<O2Checker>();
		animator = GetComponentInChildren<Animator>();
		startPoint = gameObject.transform.position;
		gravityScaleOfStartTime = GetComponent<Rigidbody2D> ().gravityScale;
		yOfLowestObject = LowestObjectFinder.Find ().position.y;
		climber = new Climber (gameObject, ladderCheckerUp, ladderCheckerDown, groundChecker, climbSpeed);
	}

	void Update ()
	{
		if (IsUnderwater() && !O2Checker.IsActive())
			O2Checker.Active();
		if (!IsUnderwater() && O2Checker.IsActive())
			O2Checker.Deactive();

		if(gameObject.transform.position.y - yOfLowestObject <= -10 || Input.GetKeyDown(KeyCode.R))
		{
			Restarter.RestartAll();
		}

		Move ();
		Jump ();
		climber.Update ();

		animator.SetFloat("absSpeedX", Mathf.Abs(GetComponent<Rigidbody2D>().velocity.x));
		animator.SetBool("isGrounded", groundChecker.IsGrounded());
		animator.SetBool("isClimbing", climber.IsClimbing());
	}

	void Move ()
	{
		if (IsUnderwater())
		{
			if (GetComponent<Rigidbody2D>().velocity.y < -1 * maxSpeedInWater)
				GetComponent<Rigidbody2D>().velocity = new Vector2 (GetComponent<Rigidbody2D>().velocity.x, -1 * maxSpeedInWater);

			if (Input.GetKey (KeyCode.RightArrow))
			{
				GetComponent<Rigidbody2D>().velocity = new Vector2(moveSpeed * 0.5f, GetComponent<Rigidbody2D>().velocity.y);
				playerSpriteObject.transform.rotation = Quaternion.Euler(0, 180, 0);
			}
			else if (Input.GetKey(KeyCode.LeftArrow))
			{
				GetComponent<Rigidbody2D>().velocity = new Vector2(-moveSpeed * 0.5f, GetComponent<Rigidbody2D>().velocity.y);
				playerSpriteObject.transform.rotation = Quaternion.Euler(0, 0, 0);
			}
			else
			{
				GetComponent<Rigidbody2D>().velocity = new Vector2(0, GetComponent<Rigidbody2D>().velocity.y);
			}
		}
		else if (Input.GetKey (KeyCode.RightArrow))
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
		if (Input.GetKeyDown (KeyCode.Space) && IsUnderwater())
		{
			GetComponent<Rigidbody2D>().velocity = new Vector2(GetComponent<Rigidbody2D>().velocity.x, jumpPower * 0.8f);
		}
		else if (Input.GetKeyDown (KeyCode.Space) && groundChecker.IsGrounded())
		{
			GetComponent<Rigidbody2D>().velocity = new Vector2(GetComponent<Rigidbody2D>().velocity.x, jumpPower);
		}
	}

	bool IsUnderwater()
	{
		Collider2D playerCollider = GetComponent<Collider2D>();
		Collider2D[] otherColliders = Physics2D.OverlapAreaAll(playerCollider.bounds.max, playerCollider.bounds.min);
		foreach (Collider2D otherCollider in otherColliders)
		{
			if (otherCollider.gameObject.tag == "Water")
				return true;
		}
		return false;
	}

	void IRestartable.Restart()
	{
		GetComponent<Rigidbody2D>().velocity = Vector2.zero;
		gameObject.transform.position = startPoint;
		//Temporarily reset isDark in Player.cs, but it should be moved to other script.
		Global.ingame.isDark = IsDark.Light;
		climber = new Climber (gameObject, ladderCheckerUp, ladderCheckerDown, groundChecker, climbSpeed);
		GetComponent<Rigidbody2D> ().gravityScale = gravityScaleOfStartTime;
	}
}