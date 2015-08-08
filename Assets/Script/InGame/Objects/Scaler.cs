using UnityEngine;
using System.Collections;

public class Scaler : MonoBehaviour
{
	public float detectingRadius;
	float startScale;

	void Start()
	{
		startScale = this.transform.localScale.x;
	}

	void Update()
	{
		Vector3 scale = new Vector3 (startScale * detectingRadius, startScale * detectingRadius, 1);
		this.transform.localScale = scale;
	}
}
