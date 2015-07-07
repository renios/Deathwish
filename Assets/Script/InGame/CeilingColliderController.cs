using UnityEngine;
using System.Collections;

public class CeilingColliderController : MonoBehaviour
{
	public GameObject Ground;
	new Collider2D collider2D;

	void Start()
	{
		collider2D = Ground.GetComponent<Collider2D> ();
	}

	public void DisableCeiling()
	{
		collider2D.enabled = false;
	}

	public void EnableCeiling()
	{
		collider2D.enabled = true;
	}
}
