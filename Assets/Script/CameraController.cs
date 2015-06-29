using UnityEngine;
using System.Collections;

public class CameraController : MonoBehaviour
{
	public PlayerController player;
	private Vector3 fromPlayerToCamera;

	void Start ()
	{
		fromPlayerToCamera = player.gameObject.transform.position - gameObject.transform.position;
	}
	
	void LateUpdate ()
	{
		gameObject.transform.position = player.transform.position - fromPlayerToCamera;
	}
}
