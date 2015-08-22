using UnityEngine;
using System.Collections;

public class Chapter5Camera : MonoBehaviour {
	void LateUpdate()
	{
		Player[] players = GameObject.FindObjectsOfType<Player>();
		float totalX = 0;
		float totalY = 0;
		
		foreach (var player in players)
		{
			totalX += player.transform.position.x;
			totalY += player.transform.position.y;
		}
		
		float averageX = totalX / players.Length;
		float averageY = totalY / players.Length;
		
		transform.position = new Vector3(averageX, averageY, transform.position.z);
	}
}
