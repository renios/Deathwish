using UnityEngine;
using System.Collections;

public class CameraController : MonoBehaviour
{
	public Player player;
	private Vector3 fromPlayerToCamera;
	public float maxDistanceX = 8f/2f;
	public float maxDistanceY = 5f/2f;
	public float moveSpeed = 0.1f;
	private Vector3 distanceByMove;

	public void MoveLeft()
	{
		if (distanceByMove.x > -1 * maxDistanceX)
			distanceByMove += Vector3.left * moveSpeed;
	}

	public void MoveRight()
	{
		if (distanceByMove.x < maxDistanceX)
			distanceByMove += Vector3.right * moveSpeed;
	}

	public void MoveUp()
	{
		if (distanceByMove.y < maxDistanceY)
			distanceByMove += Vector3.up * moveSpeed;
	}

	public void MoveDown()
	{
		if (distanceByMove.y > -1 * maxDistanceY)
			distanceByMove += Vector3.down * moveSpeed;
	}

	public void ReturnToCenter()
	{
		distanceByMove = Vector3.zero;
	}

	void Start ()
	{
		distanceByMove = Vector3.zero;
		fromPlayerToCamera = player.gameObject.transform.position - gameObject.transform.position;
	}
	
	void LateUpdate ()
	{
		gameObject.transform.position = player.transform.position - fromPlayerToCamera + distanceByMove; 
	}
}
