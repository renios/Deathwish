using UnityEngine;
using System.Collections;
using Enums;

class Climber
{
	private Rigidbody2D playerRigidbody;
	private Transform playerTransform;
	private LadderCheckerUp ladderCheckerUp;
	private LadderCheckerDown ladderCheckerDown;
	private GroundChecker groundChecker;
	private bool isClimbing = false;
	private float climbSpeed;
	private float gravityScale;

	//bool upOfLadder = false;
	//bool downOfLadder = false;
	
	public Climber(GameObject playerGo, LadderCheckerUp ladderCheckerUp, LadderCheckerDown ladderCheckerDown, GroundChecker groundChecker, float climbSpeed, float gravityScale)
	{
		playerRigidbody = playerGo.GetComponent<Rigidbody2D> ();
		playerTransform = playerGo.GetComponent<Transform> ();
		this.ladderCheckerUp = ladderCheckerUp;
		this.ladderCheckerDown = ladderCheckerDown;
		this.groundChecker = groundChecker;
		this.climbSpeed = climbSpeed;
		this.gravityScale = playerRigidbody.gravityScale;
	}
	
	public LadderCheckerUp GetLadderCheckerUp()
	{
		return ladderCheckerUp;
	}

	public LadderCheckerDown GetLadderCheckerDown()
	{
		return ladderCheckerDown;
	}

	public bool IsClimbing()
	{
		return isClimbing;
	}

	public void Update()
	{
		if (Global.ingame.GetIsDarkInPosition(playerTransform.gameObject) == IsDark.Light)
		{
			UpdateIsClimb ();
			
			if (isClimbing)
			{
				bool isMoved = MoveUpDown ();
				if (isMoved == false)
					StayInLadder ();
			}
			else
			{
				if (ladderCheckerUp.IsUpLaddered() == false && Input.GetKey(KeyCode.UpArrow))
					isClimbing = false;
				if (ladderCheckerDown.IsDownLaddered() == false && Input.GetKey (KeyCode.DownArrow))
					isClimbing = false;
			}
		}
		else
		{
			isClimbing = false;
			playerRigidbody.gravityScale = gravityScale;
		}
	}
	
	void UpdateIsClimb ()
	{
		if (isClimbing == false)
		{
			if(ladderCheckerUp.IsUpLaddered() && Input.GetKey (KeyCode.UpArrow))
			{
				isClimbing = true;
				OnStartClimb();
			}
			else if(ladderCheckerDown.IsDownLaddered() && Input.GetKey (KeyCode.DownArrow))
			{
				isClimbing = true;
				OnStartClimb();
			}
		}
		else if (isClimbing == true)
		{
			if ((ladderCheckerUp.IsUpLaddered() || ladderCheckerDown.IsDownLaddered()) == false || groundChecker.IsGrounded())
			{
				isClimbing = false;
				OnStopClimb();
			}
            if (Input.GetKeyDown(KeyCode.Space))
            {
                isClimbing = false;
            }
        }
    }
	
	bool MoveUpDown ()
	{
		if (Input.GetKey (KeyCode.UpArrow))
		{
			playerRigidbody.velocity = new Vector2 (0, climbSpeed);
			playerRigidbody.gameObject.GetComponent<Player>().soundTypePlayedAtCurrentFrame = SoundType.ClimbingLadder;
			return true;
		}
		else if (Input.GetKey (KeyCode.DownArrow))
		{
			playerRigidbody.velocity = new Vector2 (0, -climbSpeed);
			playerRigidbody.gameObject.GetComponent<Player>().soundTypePlayedAtCurrentFrame = SoundType.ClimbingLadder;
			return true;
		}
		return false;
	}
	
	void StayInLadder ()
	{
		if (groundChecker.IsGrounded () != true)
		{
			playerRigidbody.velocity = new Vector2 (0, 0);
			playerRigidbody.gravityScale = 0;
		}
	}
	
	void OnStartClimb()
	{
		Debug.Log ("OnStartClimb");
		playerRigidbody.gravityScale = 0;
		Collider2D ladderToClimb = ladderCheckerUp.GetLadderCollider ();
		if (ladderToClimb == null)
		{
			ladderToClimb = ladderCheckerDown.GetLatestLadderCollider ();
		}
		SetPositionXAtCenterOfLadder(ladderToClimb);
		ladderToClimb.gameObject.GetComponent<CeilingColliderController>().DisableCeiling();
	}
	
	void OnStopClimb()
	{
		Debug.Log ("OnStopClimb");
		playerRigidbody.gravityScale = gravityScale;
		Collider2D ladderToClimb = ladderCheckerDown.GetLatestLadderCollider ();
		if (ladderToClimb == null)
		{
			ladderToClimb = ladderCheckerUp.GetLadderCollider ();
		}
		ladderToClimb.gameObject.GetComponent<CeilingColliderController> ().EnableCeiling ();
		playerRigidbody.velocity = new Vector2 (0, 0);
	}
	
	void SetPositionXAtCenterOfLadder(Collider2D ladderCollider)
	{
		float ladderX = ladderCollider.gameObject.transform.position.x;
		Vector3 playerPosition = playerTransform.position;
		playerPosition.x = ladderX;
		playerTransform.position = playerPosition;
	}
}
