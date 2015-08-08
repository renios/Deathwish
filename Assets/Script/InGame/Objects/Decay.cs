using UnityEngine;
using System.Collections;

public class Decay : ObjectMonoBehaviour, IRestartable
{
	public float delay;
	public Sprite normal;
	public Sprite transparent;
	new private Collider2D collider2D;
	new private SpriteRenderer renderer;
	private SpriteSwitch spriteSwitch;
	bool isDestroy = false;

	void Start()
	{
		collider2D = GetComponent<Collider2D> ();
		renderer = GetComponent<SpriteRenderer> ();
		spriteSwitch = GetComponent<SpriteSwitch> ();
	}

	public override void UpdateByParent()
	{
		if (!isDestroy)
		{
			collider2D.enabled = true;
			renderer.enabled = true;
		}
	}

	void OnCollisionEnter2D(Collision2D collision)
	{
		if(collision.gameObject.tag == "Player" && Global.ingame.isDark == Enums.IsDark.Light)
		{
			Invoke("DestroySelf", delay);
			GetComponent<DecayGroundEffect>().PlayDecayEffect();
		}
	}

	void DestroySelf()
	{
		isDestroy = true;
		collider2D.enabled = false;
		renderer.sprite = transparent;
		spriteSwitch.enabled = false;
	}

	void IRestartable.Restart()
	{
		isDestroy = false;
		collider2D.enabled = true;
		renderer.sprite = normal;
		spriteSwitch.enabled = true;
	}
}
