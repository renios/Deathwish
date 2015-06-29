using UnityEngine;
using System.Collections;

public class Decay : MonoBehaviour
{
	public float delay;

	void OnCollisionEnter2D(Collision2D collision)
	{
		if(collision.gameObject.tag == "Player" && Global.ingame.isDark == Enums.IsDark.Light)
		{
			Invoke("DestroySelf", delay);
		}
	}

	void DestroySelf()
	{
		Destroy (gameObject);
	}
}
