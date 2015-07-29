using UnityEngine;
using System.Collections;

class Climber
{
	private Rigidbody2D playerRigidbody;
	private Transform playerTransform;
	private LadderChecker ladderChecker;
	private GroundChecker groundChecker;
	private bool isClimbing = false;
	private float climbSpeed;
	private float gravityScale;
	
	public Climber(GameObject playerGo, LadderChecker ladderChecker, GroundChecker groundChecker, float climbSpeed)
	{
		playerRigidbody = playerGo.GetComponent<Rigidbody2D> ();
		playerTransform = playerGo.GetComponent<Transform> ();
		this.ladderChecker = ladderChecker;
		this.groundChecker = groundChecker;
		this.climbSpeed = climbSpeed;
		this.gravityScale = playerRigidbody.gravityScale;
	}
	
	public void Update()
	{
		UpdateIsClimb ();
		
		if (isClimbing)
		{
			bool isMoved = MoveUpDown ();
			if (isMoved == false)
				StayInLadder ();
		}
	}
	
	void UpdateIsClimb ()
	{
		if (isClimbing == false)
		{
			if (ladderChecker.IsLaddered() && (Input.GetKey (KeyCode.UpArrow) || Input.GetKey(KeyCode.DownArrow)))
			{
				isClimbing = true;
				OnStartClimb();
			}
		}
		else if (isClimbing == true)
		{
			if (ladderChecker.IsLaddered() == false || groundChecker.IsGrounded())
			{
				isClimbing = false;
				OnStopClimb();
			}
		}
	}
	
	bool MoveUpDown ()
	{
		if (Input.GetKey (KeyCode.UpArrow))
		{
			playerRigidbody.velocity = new Vector2 (0, climbSpeed);
			return true;
		}
		else if (Input.GetKey (KeyCode.DownArrow))
		{
			playerRigidbody.velocity = new Vector2 (0, -climbSpeed);
			return true;
		}
		return false;
	}
	
	void StayInLadder ()
	{
		if (groundChecker.IsGrounded () != true)
		{
			playerRigidbody.velocity = new Vector2 (0, 0);
		}
	}
	
	void OnStartClimb()
	{
		Debug.Log ("OnStartClimb");
		playerRigidbody.gravityScale = 0;
		Collider2D ladderToClimb = ladderChecker.GetLadderCollider ();
		SetPositionXAtCenterOfLadder(ladderToClimb);
		ladderToClimb.gameObject.GetComponent<CeilingColliderController>().DisableCeiling();
	}
	
	void OnStopClimb()
	{
		Debug.Log ("OnStopClimb");
		playerRigidbody.gravityScale = gravityScale;
		Collider2D ladderToClimb = ladderChecker.GetLatestLadderCollider ();
		ladderToClimb.gameObject.GetComponent<CeilingColliderController> ().EnableCeiling ();
	}
	
	void SetPositionXAtCenterOfLadder(Collider2D ladderCollider)
	{
		float ladderX = ladderCollider.gameObject.transform.position.x;
		Vector3 playerPosition = playerTransform.position;
		playerPosition.x = ladderX;
		playerTransform.position = playerPosition;
	}
}
