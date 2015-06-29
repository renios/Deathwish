using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Enums;

public class Player : MonoBehaviour
{
	public float moveSpeed;
	public float jumpPower;

	private CharacterLocation characterLocation;
	new private Rigidbody2D rigidbody2D;

	void Start ()
	{
		rigidbody2D = gameObject.GetComponent<Rigidbody2D> ();
	}

	void Update ()
	{
		Move ();
		Jump ();
	}

	void OnCollisionEnter2D (Collision2D collision)
	{
		if(collision.gameObject.tag == "Ground")
		{
			characterLocation = CharacterLocation.OnBlock;
		}
	}

	void Move ()
	{
		float speed = Input.GetAxis ("Horizontal") * moveSpeed * Time.deltaTime;
		rigidbody2D.AddForce (new Vector2 (speed, 0));
	}

	void Jump ()
	{
		if (characterLocation == CharacterLocation.OnBlock && Input.GetKeyDown (KeyCode.Space))
		{
			rigidbody2D.AddForce (new Vector2 (0, jumpPower));
			characterLocation = CharacterLocation.OnAir;
		}
	}
}