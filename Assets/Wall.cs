using UnityEngine;
using System.Collections;

public class Wall : MonoBehaviour {

	// Update is called once per frame
	void Update () {
		if ((GetComponent<LightState>().GetLightState() == LightState.IsLight.False) &&
			(GetComponent<LightState>().IsAttachedLightBug() == LightState.AttachedLightBug.True))
			GetComponent<Collider2D>().enabled = false;
		else
			GetComponent<Collider2D>().enabled = true;
	}
}
