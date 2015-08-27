using UnityEngine;
using System.Collections;

public class FireSound : MonoBehaviour
{
	void Start()
	{
		GetComponentInChildren<PlayerDetector> ().SetCallBack (PlayFireIsCloseSound);
	}

	public void PlayFireIsCloseSound()
	{
		SoundEffectController soundEffectController
			= GameObject.FindObjectOfType (typeof(SoundEffectController)) as SoundEffectController;
		soundEffectController.Play (Enums.SoundType.FireIsClose);
	}
}
