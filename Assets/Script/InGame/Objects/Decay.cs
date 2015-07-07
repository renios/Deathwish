using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Decay : MonoBehaviour, IRestartable
{
	public float delay;
	public Sprite normal;
	public Sprite transparent;
	new private Collider2D collider2D;
	new private SpriteRenderer renderer;
	private SpriteSwitch spriteSwitch;

	void Start()
	{
		collider2D = GetComponent<Collider2D> ();
		renderer = GetComponent<SpriteRenderer> ();
		spriteSwitch = GetComponent<SpriteSwitch> ();
	}

	void OnCollisionEnter2D(Collision2D collision)
	{
		if(collision.gameObject.tag == "Player" && Global.ingame.isDark == Enums.IsDark.Light)
		{
			Invoke("DestroySelf", delay);
		}
	}

	void DestroySelf()
	{
		collider2D.enabled = false;
		renderer.sprite = transparent;
		spriteSwitch.enabled = false;
	}

	void IRestartable.Restart()
	{
		collider2D.enabled = true;
		renderer.sprite = normal;
		spriteSwitch.enabled = true;
	}
}
