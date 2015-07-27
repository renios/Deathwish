using UnityEngine;
using System.Collections;

public class CameraController : MonoBehaviour
{
	public GameObject player;
	public float xLimit;
	public float yLimit;

	private Vector3 startPoint;

	void Start ()
	{
		startPoint = player.transform.position - new Vector3 (0,0,10);

		gameObject.transform.position = startPoint;
	}

	void Update ()
	{
		Vector3 cameraPosition = gameObject.transform.position;
		float playerX = player.transform.position.x;
		float playerY = player.transform.position.y;

		if (cameraPosition.x - playerX > xLimit)
		{
			cameraPosition.x = playerX + xLimit;
		}

		if (cameraPosition.x + xLimit < playerX)
		{
			cameraPosition.x = playerX - xLimit;
		}

		if (cameraPosition.y - playerY > yLimit)
		{
			cameraPosition.y = playerY + yLimit;
		}

		if (cameraPosition.y + yLimit < playerY)
		{
			cameraPosition.y = playerY - yLimit;
		}

		gameObject.transform.position = cameraPosition;
	}
}