using UnityEngine;
using System.Collections;
using Enums;

public class WindSound : MonoBehaviour
{
	public float soundAreaModifier;
	new BoxCollider2D collider;
	SoundEffectController soundEffectController;

	void Start ()
	{
		collider = GetComponent<BoxCollider2D> ();
		GameObject windObject = gameObject.transform.parent.gameObject;
		Wind wind = windObject.GetComponentInChildren<Wind> ();
		BoxCollider2D windColl = wind.gameObject.GetComponent<BoxCollider2D> ();
		Vector2 soundAreaSize = new Vector2 (soundAreaModifier * windColl.size.x, collider.size.y);
		collider.size = soundAreaSize;
		soundEffectController
			= GameObject.FindObjectOfType (typeof(SoundEffectController)) as SoundEffectController;
	}

	void OnTriggerStay2D(Collider2D coll)
	{
		if(coll.gameObject.tag == "Player")
		{
			soundEffectController.Play(SoundType.WindIsClose);
		}
	}
}
