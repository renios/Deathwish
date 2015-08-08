using UnityEngine;
using System.Collections;

public class Scaler : MonoBehaviour
{
	public float detectingRadius;
	float startScale;
	SpriteRenderer spriteRenderer;

	void Start()
	{
		startScale = this.transform.localScale.x;
		spriteRenderer = GetComponent<SpriteRenderer> ();
	}

	void Update()
	{
		Vector3 scale = new Vector3 (startScale * detectingRadius, startScale * detectingRadius, 1);
		this.transform.localScale = scale;
	}
}
