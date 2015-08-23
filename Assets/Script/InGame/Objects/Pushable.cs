using UnityEngine;
using System.Collections;
using Enums;

public class Pushable : MonoBehaviour, IRestartable
{

	private Vector3 originalPosition;
	public bool isLamp;
	
	// Use this for initialization
	void Start()
	{
		originalPosition = gameObject.transform.position;
	}
	
	// Update is called once per frame
	void Update ()
	{
		if(isLamp == true)
		{
			if(GetComponent<Lamp>().lampProperty == LampProperty.DarkLamp)
			{
				gameObject.GetComponent<Rigidbody2D>().isKinematic = false;
				return;
			}
		}

		if (Global.ingame.GetIsDarkInPosition(gameObject) == IsDark.Light)
		{
			gameObject.GetComponent<Rigidbody2D>().isKinematic = false;	
		}
		else if (Global.ingame.GetIsDarkInPosition(gameObject) == IsDark.Dark)
		{
			gameObject.GetComponent<Rigidbody2D>().isKinematic = true;
		}
	}

	void IRestartable.Restart()
	{
		gameObject.transform.position = originalPosition;
		gameObject.GetComponent<Rigidbody2D>().isKinematic = false;
		SpriteSwitch spriteSwitch = gameObject.GetComponent<SpriteSwitch>();
		if(spriteSwitch != null)
			gameObject.GetComponent<SpriteRenderer>().sprite = spriteSwitch.light;
	}
}
