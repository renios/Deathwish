using UnityEngine;
using System.Collections;
using Enums;

public class Effect : MonoBehaviour, IRestartable
{
	public GameObject Effect_Light;
	public GameObject Effect_Dark;

	void Start ()
	{
		Effect_Light.SetActive(true);
		Effect_Dark.SetActive(false);
	}
	
	void Update()
	{
		if (Global.ingame.GetIsDarkInPosition(gameObject) == IsDark.Light)
		{
			Effect_Light.SetActive(true);
			Effect_Dark.SetActive(false);
		}
		else if (Global.ingame.GetIsDarkInPosition(gameObject) == IsDark.Dark)
		{
			Effect_Light.SetActive(false);
			Effect_Dark.SetActive(true);
		}
	}
	
	void IRestartable.Restart()
	{
		Effect_Light.SetActive(true);
		Effect_Dark.SetActive(false);
	}
}

