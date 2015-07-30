using UnityEngine;
using System.Collections;

public class AttachToObjectCollider : MonoBehaviour {

	void OnTriggerEnter2D(Collider2D other)
	{
		if ((other.gameObject.tag != "Player") && (other.gameObject.GetComponent<LightState>() != null))
			other.GetComponent<LightState>().AttachLightBug();
	}
	
	void OnTriggerExit2D(Collider2D other)
	{
		if ((other.gameObject.tag != "Player") && (other.gameObject.GetComponent<LightState>() != null))
			other.GetComponent<LightState>().DetachLightBug();
	}

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
