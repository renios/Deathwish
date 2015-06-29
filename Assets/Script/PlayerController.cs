﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Enums;

public class PlayerController : MonoBehaviour
{
	public float moveSpeed;
	public float jumpPower;
	public characterLocation Hover;
	public WorldControl controller;
	Rigidbody2D rg;

	void Start ()
	{
		rg = gameObject.GetComponent<Rigidbody2D> ();
		controller = FindObjectOfType (typeof(WorldControl)) as WorldControl;
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