using UnityEngine;
using System.Collections;

public class FireSound : MonoBehaviour
{
	void Start()
	{
		GetComponentInChildren<PlayerDetector> ().SetCallBack (PlayFireIsCloseSound, alsoIncludeStay: true);
	}

	public void PlayFireIsCloseSound()
	{
		if(Global.ingame.GetIsDarkInPosition(gameObject) == Enums.IsDark.Light)
		{
			SoundEffectController soundEffectController
				= GameObject.FindObjectOfType (typeof(SoundEffectController)) as SoundEffectController;
			soundEffectController.Play (Enums.SoundType.FireIsClose);
		}
	}
}
