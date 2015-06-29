using UnityEngine;
using System.Collections;

public class Decay : MonoBehaviour
{
	void OnCollisionEnter2D(Collision2D collision)
	{
		if(collision.gameObject.tag == "Player")
		{
			Destroy (gameObject);
		}
	}
}
