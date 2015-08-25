using UnityEngine;
using System.Collections;
using Enums;

public class WindEffect : MonoBehaviour, IRestartable
{
	public GameObject windEffect;
	
	void Start ()
	{
		windEffect.SetActive(true);
	}
	
	void Update()
	{
		if (Global.ingame.GetIsDarkInPosition(gameObject) == IsDark.Light)
		{
			windEffect.SetActive (true);
		}
		else if (Global.ingame.GetIsDarkInPosition(gameObject) == IsDark.Dark)
		{
			windEffect.SetActive (false);
		}
	}
	
	void IRestartable.Restart()
	{
		windEffect.SetActive (true);
	}
}