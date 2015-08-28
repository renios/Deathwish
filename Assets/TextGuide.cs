using UnityEngine;
using System.Collections;

public class TextGuide : MonoBehaviour {

	public GameObject shoutZone;
	
	private bool isAlreadyActive = false;
	private SpriteRenderer renderer;

	// Use this for initialization
	void Start () {
		renderer = gameObject.GetComponent<SpriteRenderer>();
		renderer.enabled = false;
	}
	
	// Update is called once per frame
	void Update () {
		if ((isAlreadyActive == false) && (shoutZone == null) && 
			(GameObject.FindObjectOfType<TextBoxManager>().isActive == true))
		{
			isAlreadyActive = true;
			renderer.enabled = true;
		}

		if ((renderer.enabled = true) && (GameObject.FindObjectOfType<TextBoxManager>().isActive == false))
			renderer.enabled = false;
	}
}
