using UnityEngine;
using System.Collections;
using System;

public class PlayerDetector : MonoBehaviour {

	Action callBackMethod;
	private bool alsoIncludeStay = false;

	public void SetCallBack(Action Method, bool alsoIncludeStay = false)
	{
		callBackMethod = Method;
		this.alsoIncludeStay = alsoIncludeStay;
	}

	void OnTriggerEnter2D(Collider2D other)
	{
		if (other.gameObject.tag == "Player")
		{
			callBackMethod();
		}
	}
	
	void OnTriggerStay2D(Collider2D other)
	{
		if (alsoIncludeStay && other.gameObject.tag == "Player")
		{
			callBackMethod();
		}
	}
}
