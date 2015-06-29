using UnityEngine;
using System.Collections;
using Commons;

public class WorldSwitch : MonoBehaviour
{
	public WorldControl controller;

	void Start()
	{
		controller = FindObjectOfType (typeof(WorldControl)) as WorldControl;
	}

	void OnCollisionEnter2D(Collision2D collision)
	{
		if(collision.gameObject.tag == "Player")
		{
			controller.WorldChange();
		}
	}
}
