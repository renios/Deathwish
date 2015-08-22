using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Chapter5Camera : MonoBehaviour {
	
	private Dictionary<Player, Vector3> previousPosition = new Dictionary<Player, Vector3>();
	
	void Start()
	{
		Player[] players = GameObject.FindObjectsOfType<Player>();
		foreach (var player in players)
		{
			previousPosition[player] = player.transform.position;
		}
	}

	void LateUpdate()
	{
		Player[] players = GameObject.FindObjectsOfType<Player>();
		Move(players);
		
		BackPlayer(players);
		
		foreach (var player in players)
		{
			previousPosition[player] = player.transform.position;
		}
	}
	
	private void Move(Player[] players)
	{
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
	
	private void BackPlayer(Player[] players)
	{
		var isRightMost = new Dictionary<Player, bool>();
		var isLeftMost = new Dictionary<Player, bool>();
		
		var camera = GetComponent<Camera>();
		foreach (var player in players)
		{
			var x = camera.WorldToScreenPoint(player.transform.position).x;
			isRightMost[player] = (x / Screen.width) > 0.95;
			isLeftMost[player] = (x / Screen.width) < 0.05;
		}

		var isAnyMoved = false;
		foreach (var player in players)
		{
			var diffX = player.transform.position.x - previousPosition[player].x;
			if (diffX > 0)
			{
				if (!isRightMost[player])
				{
					isAnyMoved = true;
				}
			}

			if (diffX < 0)
			{
				if (!isLeftMost[player])
				{
					isAnyMoved = true;
				}
			}
		}

		if (!isAnyMoved)
		{
			foreach (var player in players)
			{
				player.transform.position = new Vector3(previousPosition[player].x,
					player.transform.position.y,
					player.transform.position.z);
			}
		}
	}
}
