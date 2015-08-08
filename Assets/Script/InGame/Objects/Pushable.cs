using UnityEngine;
using System.Collections;
using Enums;

public class Pushable : ObjectMonoBehaviour, IRestartable {

	private Vector3 originalPosition;
	
	// Use this for initialization
	void Start () {
		originalPosition = gameObject.transform.position;
	}
	
	// Update is called once per frame
	public override void UpdateByParent () {
		if (isDarkAfterLamp() == IsDark.Light)
		{
			gameObject.GetComponent<Rigidbody2D>().isKinematic = false;	
		}
		else if (isDarkAfterLamp() == IsDark.Dark)
		{
			gameObject.GetComponent<Rigidbody2D>().isKinematic = true;
		}
	}

	public override void HideObject()
	{
		GetComponent<SpriteRenderer>().enabled = false;
		GetComponent<Collider2D>().enabled = false;
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
