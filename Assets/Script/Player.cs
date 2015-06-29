using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Enums;

public class Player : MonoBehaviour
{
	public float moveSpeed;
	public float jumpPower;

	private characterLocation Hover;
	private Rigidbody2D rg;

	void Start ()
	{
		rg = gameObject.GetComponent<Rigidbody2D> ();
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
			Hover = characterLocation.OnBlock;
		}
	}

	void Move ()
	{
		float speed = Input.GetAxis ("Horizontal") * moveSpeed * Time.deltaTime;
		rg.AddForce (new Vector2 (speed, 0));
	}

	void Jump ()
	{
		if (Hover == characterLocation.OnBlock && Input.GetKeyDown (KeyCode.Space))
		{
			rg.AddForce (new Vector2 (0, jumpPower));
			Hover = characterLocation.OnAir;
		}
	}
}