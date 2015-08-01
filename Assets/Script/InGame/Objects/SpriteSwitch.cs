using UnityEngine;
using System.Collections;
using Enums;

public class SpriteSwitch : MonoBehaviour
{

	public new Sprite light;
	public Sprite dark;

	private SpriteRenderer sr;

	void Start ()
	{
		sr = GetComponent<SpriteRenderer> ();
	}

	void Update ()
	{
		if ((GetComponent<LightState>() != null) && 
			(GetComponent<LightState>().GetLightState() == LightState.IsLight.False) &&
			(GetComponent<LightState>().IsAttachedLightBug() == LightState.AttachedLightBug.True))
			sr.enabled = false;
		else
		{
			sr.enabled = true;
			if(Global.ingame.isDark == IsDark.Light)
			{
				sr.sprite = light;
			}
			else if(Global.ingame.isDark == IsDark.Dark)
			{
				sr.sprite = dark;
			}
		}
	}
}
