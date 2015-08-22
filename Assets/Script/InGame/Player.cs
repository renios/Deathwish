using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Enums;

public class Player : MonoBehaviour, IRestartable
{
	public float moveSpeed;
	public float jumpPower;
	public float climbSpeed;
	public float windSpeed;
	public float maxSpeedInWater;
	// Temporary value.
	public float verticalDragInWater;
	public float maxSpeedInAir;

	public GroundChecker groundChecker;
	public LadderCheckerUp ladderCheckerUp;
	public LadderCheckerDown ladderCheckerDown;
	public GameObject playerSpriteObject;
	public SoundEffectController soundEffectController;
	//need for landing sound effect
	public WindDirection windDirection;

	private Vector3 startPoint;
	private float yOfLowestObject;
	private Animator animator;
	private Climber climber;
	private O2Checker O2Checker;

	float gravityScaleOfStartTime;
	bool onAir = true;
	bool leavingGround = true;

	public bool canMove;

	//used for Text display purposes.

	void Start ()
	{
		O2Checker = FindObjectOfType<O2Checker>();
		animator = GetComponentInChildren<Animator>();
		startPoint = gameObject.transform.position;
		gravityScaleOfStartTime = GetComponent<Rigidbody2D> ().gravityScale;
		yOfLowestObject = ObjectFinder.FindLowest ().position.y;
		climber = new Climber (gameObject, ladderCheckerUp, ladderCheckerDown, groundChecker, climbSpeed);
		soundEffectController = GetComponentInChildren<SoundEffectController> ();
		soundEffectController.player = this;
	}

	void Update ()
	{
		/*if (!canMove) 
		{
			return;
		}
		*/
		if (IsUnderwater() && !O2Checker.IsActive())
			O2Checker.Active();
		if (!IsUnderwater() && O2Checker.IsActive())
			O2Checker.Deactive();

		if(gameObject.transform.position.y - yOfLowestObject <= -10 || Input.GetKeyDown(KeyCode.R))
		{
			Restarter.RestartAll();
		}

		soundEffectController.characterAction = CharacterAction.Default;

		Wind ();
		Move ();
		ApplyDirectionToSprite();
		Jump ();
		CheckLandingForSoundEffect ();
		climber.Update ();

		animator.SetFloat("absSpeedX", Mathf.Abs(GetComponent<Rigidbody2D>().velocity.x));
		animator.SetBool("isGrounded", groundChecker.IsGrounded());
		animator.SetBool("isClimbing", climber.IsClimbing());

		soundEffectController.Play ();
	}

	float GetDrag(Direction direction)
	{
		if (IsUnderwater())
		{
			if (direction == Direction.Horizontal)
			{
				return 0.4f;
			}
			else // direction == Direction.Vertical
			{
				// temporary value.
				return verticalDragInWater;
				//  return 0.8f;
			}
		}
		else
			return 1;
	}

	void ApplyDirectionToSprite()
	{
		if (Input.GetKey (KeyCode.RightArrow))
			playerSpriteObject.transform.rotation = Quaternion.Euler(0, 180, 0);
		else if (Input.GetKey(KeyCode.LeftArrow))
			playerSpriteObject.transform.rotation = Quaternion.Euler(0, 0, 0);
	}

	void Move ()
	{
		if(Global.ingame.inWind == false)
		{
			if ((IsUnderwater ()) && (GetComponent<Rigidbody2D> ().velocity.y < -1 * maxSpeedInWater))
				GetComponent<Rigidbody2D> ().velocity = new Vector2 (GetComponent<Rigidbody2D> ().velocity.x, -1 * maxSpeedInWater);
			
			if ((!groundChecker.IsGrounded ()) && (GetComponent<Rigidbody2D> ().velocity.y < -1 * maxSpeedInAir))
				GetComponent<Rigidbody2D> ().velocity = new Vector2 (GetComponent<Rigidbody2D> ().velocity.x, -1 * maxSpeedInAir);
			
			if (Input.GetKey (KeyCode.RightArrow))
			{
				GetComponent<Rigidbody2D> ().velocity = new Vector2 (moveSpeed * GetDrag (Direction.Horizontal), GetComponent<Rigidbody2D> ().velocity.y);
				if(!onAir)
				{
					soundEffectController.characterAction = CharacterAction.Walk;
				}
			}
			else if (Input.GetKey (KeyCode.LeftArrow))
			{
				GetComponent<Rigidbody2D> ().velocity = new Vector2 (-moveSpeed * GetDrag (Direction.Horizontal), GetComponent<Rigidbody2D> ().velocity.y);
				if(!onAir)
				{
					soundEffectController.characterAction = CharacterAction.Walk;
				}
			}
			else
				GetComponent<Rigidbody2D>().velocity = new Vector2(0, GetComponent<Rigidbody2D>().velocity.y);
		}
		}

	private void Jump()
	{
		if (Input.GetKeyDown (KeyCode.Space) && IsUnderwater())
		{
			GetComponent<Rigidbody2D>().velocity = new Vector2(GetComponent<Rigidbody2D>().velocity.x, jumpPower * GetDrag(Direction.Vertical));
		}
		else if (Input.GetKeyDown (KeyCode.Space) && groundChecker.IsGrounded())
		{
			GetComponent<Rigidbody2D>().velocity = new Vector2(GetComponent<Rigidbody2D>().velocity.x, jumpPower);
		}
		else {return;}

		soundEffectController.characterAction = CharacterAction.Jump;
		onAir = true;
	}

	void OnCollisionEnter2D(Collision2D collision)
	{
		Global.ingame.inWind = false;
	}

	void Wind()
	{
		if (Global.ingame.inWind == true)
		{
			GetComponent<Rigidbody2D>().gravityScale = 0;
			if (windDirection == WindDirection.Left)
			{
				GetComponent<Rigidbody2D>().velocity = new Vector2(-windSpeed,0);
			}
			else if (windDirection == WindDirection.Right)
			{
				GetComponent<Rigidbody2D>().velocity = new Vector2(windSpeed,0);
			}
			else if (windDirection == WindDirection.Up)
			{
				GetComponent<Rigidbody2D>().velocity = new Vector2(0, windSpeed);
			}
			else if (windDirection == WindDirection.Down)
			{
				GetComponent<Rigidbody2D>().velocity = new Vector2(0, -windSpeed);
			}
		}

		if (Global.ingame.inWind == false)
		{
			GetComponent<Rigidbody2D>().gravityScale = gravityScaleOfStartTime;
		}
	}

	void CheckLandingForSoundEffect()
	{
		if(onAir&&!leavingGround)
		{
			if(!groundChecker.IsGrounded())
			{
				leavingGround = true;
				return;
			}
		}

		if(onAir&&leavingGround)
		{
			if(groundChecker.IsGrounded())
			{
				soundEffectController.characterAction = CharacterAction.Land;
				onAir = false;
				leavingGround = false;
			}
		}
	}

	bool IsUnderwater()
	{
		Collider2D playerCollider = GetComponent<Collider2D>();
		Collider2D[] otherColliders = Physics2D.OverlapAreaAll(playerCollider.bounds.max, playerCollider.bounds.min);
		foreach (Collider2D otherCollider in otherColliders)
		{
			if ((otherCollider.gameObject.tag == "Water") && 
				(otherCollider.gameObject.GetComponent<Water>().IsActive()))
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
