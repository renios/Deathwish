using UnityEngine;
using System.Collections;

public class AwayFromCharacterCollider : MonoBehaviour {

	GameObject lightBug;

	void OnTriggerEnter2D(Collider2D other)
	{
		if (other.gameObject.tag == "Player")
		{
			lightBug.GetComponent<LightBug>().MoveNextPoint();
		}
	}

	// Use this for initialization
	void Start () {
		lightBug = gameObject.transform.parent.gameObject;
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
