using UnityEngine;
using System.Collections;

public class Damage : MonoBehaviour
{
	void OnCollisionEnter2D (Collision2D collision)
	{
		if (collision.gameObject.tag == "Player")
			Restarter.RestartAll();
	}
}