using UnityEngine;
using System.Collections;

public class PopupJumpButton : MonoBehaviour {

	// Use this for initialization
	void Start () {
		gameObject.GetComponent<SpriteRenderer>().enabled = false;
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnTriggerStay2D(Collider2D coll)
	{
		if (coll.gameObject.tag == "Player")
			gameObject.GetComponent<SpriteRenderer>().enabled = true;
	}

	void OnTriggerExit2D(Collider2D coll)
	{
		if (coll.gameObject.tag == "Player")
			gameObject.GetComponent<SpriteRenderer>().enabled = false;
	}
}
