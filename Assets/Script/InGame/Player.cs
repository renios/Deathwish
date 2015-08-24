using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Enums;
using System.Linq;

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
	private AllAboutO2 AllAboutO2;

	float gravityScaleOfStartTime;
	bool onAir = true;
	bool leavingGround = true;
	HashSet<GameObject> pushableObjectsNearbyPlayer;

	public bool canMove;

	public GameObject dieEffectLight;
	public GameObject dieEffectDark;

	public GravityDirection gravityDirection;

	//used for Text display purposes.

	void Start ()
	{
		pushableObjectsNearbyPlayer = new HashSet<GameObject>();
		AllAboutO2 = FindObjectOfType<AllAboutO2>();
		animator = GetComponentInChildren<Animator>();
		startPoint = gameObject.transform.position;
		GetComponent<Rigidbody2D> ().gravityScale = GetComponent<Rigidbody2D> ().gravityScale * GravityCoefficient(gravityDirection);
		gravityScaleOfStartTime = GetComponent<Rigidbody2D> ().gravityScale;
		yOfLowestObject = ObjectFinder.FindLowest ().position.y;
		climber = new Climber (gameObject, ladderCheckerUp, ladderCheckerDown, groundChecker, climbSpeed, gravityScaleOfStartTime);
		soundEffectController = GetComponentInChildren<SoundEffectController> ();
		soundEffectController.player = this;

		if(gravityDirection == GravityDirection.Reverse)
		{
			GetComponentInChildren<Camera>().enabled = false;
			GetComponentInChildren<AudioListener>().enabled = false;
			this.transform.rotation = Quaternion.Euler(180, 0, 0);
		}
	}

	void Update ()
	{
		if (!canMove) 
		{
			GetComponent<Rigidbody2D>().velocity = new Vector2(0, GetComponent<Rigidbody2D>().velocity.y);
			animator.SetFloat("absSpeedX", Mathf.Abs(GetComponent<Rigidbody2D>().velocity.x));
			return;
		}

		if (IsUnderwater() && !AllAboutO2.IsActive())
			AllAboutO2.Active();
		if (!IsUnderwater() && AllAboutO2.IsActive())
			AllAboutO2.Deactive();

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
		animator.SetBool("isDark", IsItDark ());
		animator.SetBool("isPushing", IsPlayerPushingObject());

		soundEffectController.Play ();

		IsItDark ();
	}
	
	public void PlayDieAnimAndRestart()
	{
		StartCoroutine(PlayDieAnimAndRestartCoroutine());
	}

	IEnumerator PlayDieAnimAndRestartCoroutine()
	{
		canMove = false;
		ReverseSpriteDirection();
		animator.SetTrigger("Die");
		PlayDieEffect();
		float playTimeOfAnim = 2;
		yield return new WaitForSeconds(playTimeOfAnim);
		canMove = true;
		animator.SetTrigger("Revive");
		Restarter.RestartAll();
	}

	void PlayDieEffect()
	{
		GameObject dieEffect;
		if (Global.ingame.GetIsDarkInPosition(gameObject) == IsDark.Light)
			dieEffect = Instantiate(dieEffectLight, gameObject.transform.position, transform.rotation) as GameObject;
		else
			dieEffect = Instantiate(dieEffectDark, gameObject.transform.position, transform.rotation) as GameObject;
		Destroy(dieEffect, 2);
	}

	void ReverseSpriteDirection()
	{
		float rotationY = playerSpriteObject.transform.rotation.eulerAngles.y;
		playerSpriteObject.transform.rotation = Quaternion.Euler(0, 180 - rotationY, 0);
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
		if(gravityDirection == GravityDirection.Normal)
		{
			if (Input.GetKey (KeyCode.RightArrow))
				playerSpriteObject.transform.rotation = Quaternion.Euler(0, 180, 0);
			else if (Input.GetKey(KeyCode.LeftArrow))
				playerSpriteObject.transform.rotation = Quaternion.Euler(0, 0, 0);
		}
		else
		{
			if (Input.GetKey (KeyCode.RightArrow))
				playerSpriteObject.transform.rotation = Quaternion.Euler(180, 180, 0);
			else if (Input.GetKey(KeyCode.LeftArrow))
				playerSpriteObject.transform.rotation = Quaternion.Euler(180, 0, 0);
		}
	}

	void Move ()
	{
		if(Global.ingame.inWind == false)
		{
			if ((IsUnderwater ()) && (overMaxFallingSpeed(GetComponent<Rigidbody2D> ().velocity.y, maxSpeedInWater, GravityCoefficient(gravityDirection))))
				GetComponent<Rigidbody2D> ().velocity = new Vector2 (GetComponent<Rigidbody2D> ().velocity.x, -1 * maxSpeedInWater * GravityCoefficient(gravityDirection));
			
			if ((!groundChecker.IsGrounded ()) && (overMaxFallingSpeed(GetComponent<Rigidbody2D> ().velocity.y, maxSpeedInAir, GravityCoefficient(gravityDirection))))
				GetComponent<Rigidbody2D> ().velocity = new Vector2 (GetComponent<Rigidbody2D> ().velocity.x, -1 * maxSpeedInAir * GravityCoefficient(gravityDirection));
			
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

	bool overMaxFallingSpeed(float currentSpeed, float maxFallingSpeed, int gravityCoefficient)
	{
		if(gravityCoefficient > 0)
		{
			if(currentSpeed < -1 * maxFallingSpeed * gravityCoefficient) { return true; }
			else { return false; }
		}
		else
		{
			if(currentSpeed > -1 * maxFallingSpeed * gravityCoefficient) { return true; }
			else { return false; }
		}
	}

	private void Jump()
	{
		if (Input.GetKeyDown (KeyCode.Space) && IsUnderwater())
		{
			GetComponent<Rigidbody2D>().velocity = new Vector2(GetComponent<Rigidbody2D>().velocity.x, jumpPower * GetDrag(Direction.Vertical) * GravityCoefficient(gravityDirection));
		}
		else if (Input.GetKeyDown (KeyCode.Space) && groundChecker.IsGrounded())
		{
			GetComponent<Rigidbody2D>().velocity = new Vector2(GetComponent<Rigidbody2D>().velocity.x, jumpPower * GravityCoefficient(gravityDirection));
		}
		else {return;}

		soundEffectController.characterAction = CharacterAction.Jump;
		onAir = true;
	}

	void OnCollisionEnter2D(Collision2D coll)
	{
		Global.ingame.inWind = false;

		if ((coll.gameObject.tag == "Box") || (coll.gameObject.tag == "Lamp"))
		{
			GameObject newPushableObject = coll.gameObject;
			pushableObjectsNearbyPlayer.Add(newPushableObject);
		}
	}

	void Wind()
	{
		if (Global.ingame.isDark == IsDark.Light)
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

	int GravityCoefficient(GravityDirection gravityDirection)
	{
		if(gravityDirection == GravityDirection.Normal) return 1;
		else return (-1);
	}

	bool IsItDark()
	{
		IsDark isItDark = Global.ingame.GetIsDarkInPosition (gameObject);
		if (isItDark == IsDark.Light) 
		{
			return false;
		} 
		else if (isItDark == IsDark.Dark) 
		{
			return true;
		}
		else 
		{
			return true;
		}
	}

	bool IsPlayerPushingObject()
	{
		foreach (GameObject pushableObject in pushableObjectsNearbyPlayer)
		{
			// It prevent playing 'push' animation when Player is over or under the box (or lamp).
			float maximumDeltaY = 1;
			if (Mathf.Abs(gameObject.transform.position.y - pushableObject.transform.position.y) < maximumDeltaY)
			{
				// Player -> Box
				if ((gameObject.GetComponent<Rigidbody2D>().velocity.x > 0) && (gameObject.transform.position.x < pushableObject.transform.position.x))
					return true;
		
				// Box <- Player
				if ((gameObject.GetComponent<Rigidbody2D>().velocity.x < 0) && (gameObject.transform.position.x > pushableObject.transform.position.x))
					return true;
			}
		}

		return false;
	}

	void OnCollisionExit2D(Collision2D coll)
	{
		if ((coll.gameObject.tag == "Box") || (coll.gameObject.tag == "Lamp"))
		{
			GameObject pushableObject = coll.gameObject;
			pushableObjectsNearbyPlayer.Remove(pushableObject);
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
		climber = new Climber (gameObject, ladderCheckerUp, ladderCheckerDown, groundChecker, climbSpeed, gravityScaleOfStartTime);
		GetComponent<Rigidbody2D> ().gravityScale = gravityScaleOfStartTime;
		pushableObjectsNearbyPlayer = new HashSet<GameObject>();
	}
}
