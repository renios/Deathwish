using UnityEngine;
using System.Collections;
using Enums;

public class SpriteSwitch : MonoBehaviour
{
	public new Sprite light;
	public Sprite dark;

	private SpriteRenderer spriteRenderer;

	void Start ()
	{
		spriteRenderer = GetComponent<SpriteRenderer> ();
	}

	//fatal problem. Need to solve.
	void Update ()
	{
		IsDark isDarkNow = Global.ingame.GetIsDarkInPosition(gameObject);
		if(isDarkNow == IsDark.Light)
		{
			spriteRenderer.sprite = light;
		}
		else if(isDarkNow == IsDark.Dark)
		{
			spriteRenderer.sprite = dark;
		}
	}
}
