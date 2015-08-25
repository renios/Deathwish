using UnityEngine;
using System.Collections;
using Enums;

public class WindEffect : MonoBehaviour, IRestartable
{
	public GameObject windEffect;

	Vector3 position;
	
	void Start ()
	{
		windEffect.SetActive(true);
		position = GetComponent<Transform> ().position;
	}
	
	void Update()
	{
		if (Global.ingame.GetIsDarkInPosition (position) == IsDark.Light)
		{
			windEffect.SetActive (true);
		}
		else if (Global.ingame.GetIsDarkInPosition (position) == IsDark.Dark)
		{
			windEffect.SetActive (false);
		}
	}
	
	void IRestartable.Restart()
	{
		windEffect.SetActive (true);
	}
}