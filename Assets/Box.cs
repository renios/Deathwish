using UnityEngine;
using System.Collections;
using Enums;

public class Box : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		if (Global.ingame.isDark == IsDark.Light)
		{
			gameObject.GetComponent<Rigidbody2D>().isKinematic = false;	
		}
		else if (Global.ingame.isDark == IsDark.Dark)
		{
			gameObject.GetComponent<Rigidbody2D>().isKinematic = true;
		}
	}
}
