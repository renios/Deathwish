using UnityEngine;
using System.Collections;

public class Goal : MonoBehaviour
{
	void OnCollisionEnter2D(Collision2D collision)
	{
		if(collision.gameObject.tag == "Player")
		{
			LevelEnd();
		}
	}

	//Not made yet.
	void LevelEnd()
	{
		Debug.Log ("End");
		return;
	}
}
