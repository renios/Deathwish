using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Enums;

public class Player : MonoBehaviour
{
	public float moveSpeed;
	public float jumpPower;

	private IsAlive isAlive;
	private CharacterLocation characterLocation;
	new private Rigidbody2D rigidbody2D;

	void Start ()
	{
		rigidbody2D = gameObject.GetComponent<Rigidbody2D> ();
		isAlive = IsAlive.Alive;
	}

	void Update ()
	{
		if(isAlive == IsAlive.Alive)
		{
			Move ();
			Jump ();
		}
	}

	void OnCollisionEnter2D (Collision2D collision)
	{
		if(collision.gameObject.tag == "Ground")
		{
			characterLocation = CharacterLocation.OnBlock;
		}

		if(collision.gameObject.tag == "Fire" && Global.ingame.isDark == IsDark.Light)
		{
			isAlive = IsAlive.Dead;
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