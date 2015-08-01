using UnityEngine;
using System.Collections;
using Enums;

public class SwitchDarkLight : MonoBehaviour
{
	void OnTriggerStay2D(Collider2D collision)
	{
		if ((collision.gameObject.tag == "Player") && (Input.GetKeyDown(KeyCode.UpArrow)))
		{
			foreach (LightState lightState in FindObjectsOfType(typeof(LightState)) as LightState[])
			{
				if (Global.ingame.isDark == IsDark.Light)
					lightState.GetComponent<LightState>().ChangeStateToDark();
				else
					lightState.GetComponent<LightState>().ChangeStateToLight();
			}
			
			Global.ingame.ChangeDarkLight();
		}
	}
}
