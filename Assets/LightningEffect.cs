using UnityEngine;
using System.Collections;

public class LightningEffect : MonoBehaviour {

	public float playEffectTime;

	Camera mainCamera;

	// Use this for initialization
	void Start () {
		mainCamera = Camera.main;
		iTween.FadeTo(gameObject, 0, playEffectTime);
		Destroy(gameObject, playEffectTime+1);
	}
	
	// Update is called once per frame
	void Update () {
		transform.position = mainCamera.transform.position - new Vector3(0, 0, -1);
	}
}
