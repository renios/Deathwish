using UnityEngine;
using System.Collections;

public class LightningEffect : MonoBehaviour {

	public float playEffectTime;

	Camera mainCamera;
	new SpriteRenderer renderer;

	// Use this for initialization
	void Start () {
		mainCamera = Camera.main;
		renderer = GetComponent<SpriteRenderer>();
		iTween.FadeTo(gameObject, 0, playEffectTime);
		Destroy(gameObject, playEffectTime++);
	}
	
	// Update is called once per frame
	void Update () {
		transform.position = mainCamera.transform.position - new Vector3(0, 0, -1);
	}
}
