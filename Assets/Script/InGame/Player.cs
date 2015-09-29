using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Enums;
using System.Linq;
using System;

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
	public WindDirection windDirection;

	private Vector3 startPoint;
	private float yOfLowestObject;
	private Animator animator;
	private Climber climber;
	private AllAboutO2 allAboutO2;

	float gravityScaleOfStartTime;
	HashSet<GameObject> pushableObjectsNearbyPlayer;

	private bool _canMove = true;
	public bool canMove
	{
		get {
			return _canMove;
		}
		set {
			if (value == false)
			{
				Debug.Log("CanMove set true");
				GetComponent<Rigidbody2D>().velocity = new Vector2(0, GetComponent<Rigidbody2D>().velocity.y);
			}
			_canMove = value;
		}
	}

	public GameObject dieEffectLight;
	public GameObject dieEffectDark;

	public GravityDirection gravityDirection;

	public SoundType soundTypePlayedAtCurrentFrame = SoundType.None;
	public bool withGrass = false;
	bool onAir = true;
	bool leavingGround = true;
	bool isDead = false;
	private CameraController mainCameraController;
	Vector3 pastPosition;

	//used for Text display purposes.

	void Start ()
	{
		mainCameraController = Camera.main.gameObject.GetComponent<CameraController>();
		pushableObjectsNearbyPlayer = new HashSet<GameObject>();
		allAboutO2 = FindObjectOfType<AllAboutO2>();
		animator = GetComponentInChildren<Animator>();
		startPoint = gameObject.transform.position;
		GetComponent<Rigidbody2D> ().gravityScale = GetComponent<Rigidbody2D> ().gravityScale * GravityCoefficient(gravityDirection);
		gravityScaleOfStartTime = GetComponent<Rigidbody2D> ().gravityScale;
		yOfLowestObject = ObjectFinder.FindLowest ().position.y;
		climber = new Climber (gameObject, ladderCheckerUp, ladderCheckerDown, groundChecker, climbSpeed, gravityScaleOfStartTime);
		soundEffectController = GameObject.FindObjectOfType (typeof(SoundEffectController)) as SoundEffectController;
		pastPosition = startPoint;

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
			//  GetComponent<Rigidbody2D>().velocity = new Vector2(0, GetComponent<Rigidbody2D>().velocity.y);

			animator.SetFloat("absSpeedX", Mathf.Abs(GetComponent<Rigidbody2D>().velocity.x));
			animator.SetBool("isGrounded", groundChecker.IsGrounded());
			animator.SetBool("isClimbing", climber.IsClimbing());
			animator.SetBool("isDark", IsItDark ());
			animator.SetBool("isPushing", IsPlayerPushingObject());
			animator.SetBool("isObjectSmall", WhatIsPlayerPushingObject() == ObjectSize.Small);

			return;
		}

		if (IsUnderwater() && !allAboutO2.IsActive())
			allAboutO2.Active();
		if (!IsUnderwater() && allAboutO2.IsActive())
			allAboutO2.Deactive();

		if(gameObject.transform.position.y - yOfLowestObject <= -10 || Input.GetKeyDown(KeyCode.R))
		{
			Restarter.RestartAll();
		}

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
		animator.SetBool("isObjectSmall", WhatIsPlayerPushingObject() == ObjectSize.Small);

		if(IsPlayerPushingObject() && !IsUnderwater()) soundTypePlayedAtCurrentFrame = SoundType.BoxPush;

		soundEffectController.Play (soundTypePlayedAtCurrentFrame);
		soundTypePlayedAtCurrentFrame = SoundType.None;

		MoveMainCamera();
		pastPosition = transform.position;
		
		if (Input.GetKeyUp(KeyCode.C))
		{
			Scene.LoadNextStageAndSave();
			TrackClearCheat();
		}
		if (Input.GetKeyUp(KeyCode.X))
		{
			Global.ingame.ChangeDarkLight();
			Scene.LoadNextStageAndSave();
		}
		if (Input.GetKeyUp(KeyCode.Escape))
		{
			Scene.Load("SelectStage", Scene.SceneType.StageSelect);
		}
	}

	void MoveMainCamera()
	{
		if (Input.GetKeyUp(KeyCode.UpArrow) || Input.GetKeyUp(KeyCode.DownArrow) ||
			Input.GetKeyUp(KeyCode.LeftArrow) || Input.GetKeyUp(KeyCode.RightArrow) ||
			climber.IsClimbing() || !groundChecker.IsGrounded() || pastPosition != transform.position ||
			climber.GetLadderCheckerUp().IsUpLaddered() || climber.GetLadderCheckerDown().IsDownLaddered() ||
			IsOverlappedWithMirror())
		{
			mainCameraController.ReturnToCenter();
			return;
		}
		
		if (Input.GetKey(KeyCode.UpArrow))
		{
			mainCameraController.MoveUp();
		}
		else if (Input.GetKey(KeyCode.DownArrow))
		{
			mainCameraController.MoveDown();
		}
		else if (Input.GetKey(KeyCode.LeftArrow))
		{
			//  mainCameraController.MoveLeft();
		}
		else if (Input.GetKey(KeyCode.RightArrow))
		{
			//  mainCameraController.MoveRight();
		}
	}

	bool IsOverlappedWithMirror()
	{
		Collider2D coll = GetComponent<Collider2D>();
		var otherColliders = Physics2D.OverlapAreaAll(coll.bounds.max, coll.bounds.min);

		return otherColliders.Any(k => k.GetComponent<SwitchDarkLight>() != null);
	}

    private void TrackClearCheat()
    {
        if (GoogleAnalyticsV3.getInstance() == null)
		{
			return;
		}

		GoogleAnalyticsV3.getInstance().LogEvent("cheat", "clear", Scene.currentSceneName.ToString(), 1);
    }

    public void PlayDieAnimSoundAndRestart(SoundType soundType)
	{
		if (isDead) return;
		StartCoroutine(PlayDieAnimSoundAndRestartCoroutine(soundType));
	}

	IEnumerator PlayDieAnimSoundAndRestartCoroutine(SoundType soundType)
	{
		isDead = true;
		canMove = false;
		soundEffectController.Play (soundType);
		ReverseSpriteDirection();
		animator.SetTrigger("Die");
		PlayDieEffect();
		float playTimeOfAnim = 2;
		yield return new WaitForSeconds(playTimeOfAnim);
		canMove = true;
		animator.SetTrigger("Revive");
		ReverseSpriteDirection();
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
		// Temporary fix rotating die animatino.
		// Die animation direction is reversed with character animation.
		// Teporarily rotate it in code.
		float rotationY = playerSpriteObject.transform.rotation.eulerAngles.y;
		float rotationX = playerSpriteObject.transform.rotation.eulerAngles.x;
		float rotationZ = playerSpriteObject.transform.rotation.eulerAngles.z;
		playerSpriteObject.transform.rotation = Quaternion.Euler(rotationX, 180 - rotationY, rotationZ);
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
			if ((IsUnderwater ()) && (OverMaxFallingSpeed(GetComponent<Rigidbody2D> ().velocity.y, maxSpeedInWater, GravityCoefficient(gravityDirection))))
				GetComponent<Rigidbody2D> ().velocity = new Vector2 (GetComponent<Rigidbody2D> ().velocity.x, -1 * maxSpeedInWater * GravityCoefficient(gravityDirection));
			
			if ((!groundChecker.IsGrounded ()) && (OverMaxFallingSpeed(GetComponent<Rigidbody2D> ().velocity.y, maxSpeedInAir, GravityCoefficient(gravityDirection))))
				GetComponent<Rigidbody2D> ().velocity = new Vector2 (GetComponent<Rigidbody2D> ().velocity.x, -1 * maxSpeedInAir * GravityCoefficient(gravityDirection));
			
			if (Input.GetKey (KeyCode.RightArrow))
			{
				MoveRight1Frame();
			}
			else if (Input.GetKey (KeyCode.LeftArrow))
			{
				MoveLeft1Frame();
			}
			else
				GetComponent<Rigidbody2D>().velocity = new Vector2(0, GetComponent<Rigidbody2D>().velocity.y);
		}
	}

	public void MoveRight1Frame()
	{
		GetComponent<Rigidbody2D> ().velocity = new Vector2 (moveSpeed * GetDrag (Direction.Horizontal), GetComponent<Rigidbody2D> ().velocity.y);
		if(!onAir)
		{
			if(withGrass) soundTypePlayedAtCurrentFrame = SoundType.GrassPassing;
			else soundTypePlayedAtCurrentFrame = SoundType.Walk;
		}
		if(IsUnderwater())
		{
			soundTypePlayedAtCurrentFrame = SoundType.Swim;
		}
	}

	public void MoveLeft1Frame()
	{
		GetComponent<Rigidbody2D> ().velocity = new Vector2 (-moveSpeed * GetDrag (Direction.Horizontal), GetComponent<Rigidbody2D> ().velocity.y);
		if(!onAir)
		{
			if(withGrass) soundTypePlayedAtCurrentFrame = SoundType.GrassPassing;
			else soundTypePlayedAtCurrentFrame = SoundType.Walk;
		}
		if(IsUnderwater())
		{
			soundTypePlayedAtCurrentFrame = SoundType.Swim;
		}
	}

	bool OverMaxFallingSpeed(float currentSpeed, float maxFallingSpeed, int gravityCoefficient)
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

		soundTypePlayedAtCurrentFrame = SoundType.Jump;
		onAir = true;
	}

	void OnCollisionEnter2D(Collision2D coll)
	{
		Global.ingame.inWind = false;

		if (((Global.ingame.GetIsDarkInPosition(this.gameObject) == IsDark.Light) && (coll.gameObject.tag == "Box"))
			 || (coll.gameObject.tag == "Lamp"))
		{
			GameObject newPushableObject = coll.gameObject;
			pushableObjectsNearbyPlayer.Add(newPushableObject);
		}
	}

	void Wind()
	{
		if (Global.ingame.GetIsDarkInPosition(gameObject) == IsDark.Light)
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
				soundTypePlayedAtCurrentFrame = SoundType.Land;
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
		if (Mathf.Abs(GetComponent<Rigidbody2D>().velocity.x) == 0)
			return false;

		foreach (GameObject pushableObject in pushableObjectsNearbyPlayer)
		{
			// It prevent playing 'push' animation when Player is over or under the box (or lamp).
			float maximumDeltaY = 1;
			if ((Mathf.Abs(gameObject.transform.position.y - pushableObject.transform.position.y) < maximumDeltaY) &&
				(IsSameDirectionWithPlayerVelocity(pushableObject)))
				return true;
		}

		return false;
	}

	public enum ObjectSize
	{
		Null,
		Small,
		Big
	}

	ObjectSize WhatIsPlayerPushingObject()
	{
		// It prevent playing 'push' animation when Player is over or under the box (or lamp).
		float maximumDeltaY = 1;
		// It divide big / small push animation.
		float minimumDeltaY = 0.5f;

		HashSet<GameObject> pushableObjectsAtSameDirection = new HashSet<GameObject>();
		foreach (GameObject pushableObject in pushableObjectsNearbyPlayer)
		{
			if (IsSameDirectionWithPlayerVelocity(pushableObject))
				pushableObjectsAtSameDirection.Add(pushableObject);
		}

		if (pushableObjectsAtSameDirection.Any(k => (Mathf.Abs(gameObject.transform.position.y - k.transform.position.y) < minimumDeltaY) == true))
			return ObjectSize.Big;

		if (pushableObjectsAtSameDirection.Any(k => (Mathf.Abs(gameObject.transform.position.y - k.transform.position.y) < maximumDeltaY) == true))
				return ObjectSize.Small;

		return ObjectSize.Null;
	}		

	bool IsSameDirectionWithPlayerVelocity(GameObject pushableObject)
	{
		// Player -> Box
		if ((gameObject.GetComponent<Rigidbody2D>().velocity.x > 0) && (gameObject.transform.position.x < pushableObject.transform.position.x))
			return true;

		// Box <- Player
		if ((gameObject.GetComponent<Rigidbody2D>().velocity.x < 0) && (gameObject.transform.position.x > pushableObject.transform.position.x))
			return true;

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
		isDead = false;
		climber = new Climber (gameObject, ladderCheckerUp, ladderCheckerDown, groundChecker, climbSpeed, gravityScaleOfStartTime);
		GetComponent<Rigidbody2D> ().gravityScale = gravityScaleOfStartTime;
		pushableObjectsNearbyPlayer = new HashSet<GameObject>();
		
		//FIXME: scene reseting is should moved.
		var shadowStarter = GameObject.FindObjectOfType<ShadowStarter>();
		if (shadowStarter == null) {
			Global.ingame.isDark = Enums.IsDark.Light;
		}
	}
}
