using UnityEngine;
using System.Collections;

public class CeilingColliderController : MonoBehaviour
{
	public GameObject Ground;
	new Collider2D[] collider2D;

	void Start()
	{
		collider2D = Ground.GetComponents<Collider2D> ();
	}

	public void DisableCeiling()
	{
		collider2D [0].enabled = false;
		collider2D [1].enabled = false;
	}

	public void EnableCeiling()
	{
		collider2D [0].enabled = true;
		collider2D [1].enabled = true;
	}
}
