using UnityEngine;
using System.Collections;

public class AwayFromCharacterCollider : MonoBehaviour {

	LightBug lightBug;

	public void SetReceive(LightBug lightBug)
	{
		this.lightBug = lightBug;
	}

	void OnTriggerEnter2D(Collider2D other)
	{
		if (other.gameObject.tag == "Player")
		{
			lightBug.MoveNextPoint();
		}
	}
}
