using UnityEngine;
using System.Collections;

public class MirrorGuide : MonoBehaviour {

	private SpriteRenderer renderer;

	// Use this for initialization
	void Start () {
		renderer = gameObject.GetComponent<SpriteRenderer>();
		renderer.enabled = false;
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnTriggerStay2D(Collider2D coll)
	{
		if (coll.gameObject.tag == "Player")
			renderer.enabled = true;
	}

	void OnTriggerExit2D(Collider2D coll)
	{
		if (coll.gameObject.tag == "Player")
			renderer.enabled = false;
	}	
}
