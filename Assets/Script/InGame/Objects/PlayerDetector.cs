using UnityEngine;
using System.Collections;
using System;

public class PlayerDetector : MonoBehaviour {

	Action callBackMethod;

	public void SetCallBack(Action Method)
	{
		callBackMethod = Method;
	}

	void OnTriggerEnter2D(Collider2D other)
	{
		if (other.gameObject.tag == "Player")
		{
			callBackMethod();
		}
	}
}
