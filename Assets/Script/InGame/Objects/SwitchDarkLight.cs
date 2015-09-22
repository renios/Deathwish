using UnityEngine;
using System.Collections;
using Enums;
using UnityStandardAssets.ImageEffects;

public class SwitchDarkLight : MonoBehaviour, IRestartable
{
	private bool isGround;
	private bool isPlayer = false;
	private bool isItUsedNow = false;
	private Camera blurEffectCamera;
	private BlurOptimized blur;
	IEnumerator mirrorEffectCoroutine;

	void Start()
	{
		blur = FindObjectOfType<BlurOptimized>();
		blurEffectCamera = blur.gameObject.GetComponent<Camera>();
		blurEffectCamera.enabled = false;
	}

	void Update()
	{
		isGround = GameObject.FindObjectOfType<GroundChecker> ().IsGrounded ();

		if (isPlayer && Input.GetKeyDown(KeyCode.UpArrow) && isGround && (!isItUsedNow)) //changed for mirror disabling purposes when reading dialogue.
		{
			if ((FindObjectOfType<TextBoxManager>() != null) && (FindObjectOfType<TextBoxManager>().isActive))
				return;
			mirrorEffectCoroutine = PlayMirrorEffect();
			StartCoroutine(mirrorEffectCoroutine);
		}
	}

	IEnumerator PlayMirrorEffect()
	{
		Player player = FindObjectOfType<Player>();

		isItUsedNow = true;
		player.canMove = false;
		blur.blurSize = 0;
		blurEffectCamera.enabled = true;
		
		SoundEffectController soundEffectController
			= GameObject.FindObjectOfType(typeof(SoundEffectController)) as SoundEffectController;
		soundEffectController.Play(SoundType.Mirror);
		
		for (int i = 0; i < 50; i++)
		{
			blurEffectCamera.GetComponent<BlurOptimized>().blurSize += 0.2f;
			yield return new WaitForSeconds(0.5f / 50f);
		}
		
		Global.ingame.ChangeDarkLight();

		for (int i = 0; i < 50; i++)
		{
			blurEffectCamera.GetComponent<BlurOptimized>().blurSize -= 0.2f;
			yield return new WaitForSeconds(0.5f / 50f);
		}
		
		blurEffectCamera.enabled = false;
		player.canMove = true;
		isItUsedNow = false;
	}

	void OnTriggerEnter2D(Collider2D player)
	{
		if (player.gameObject.tag == "Player")
		{
			isPlayer = true;
		}
	}

	void OnTriggerExit2D(Collider2D player)
	{
		if (player.gameObject.tag == "Player")
		{
			isPlayer = false;
		}
	}

	void OnDestroy()
	{
		if (mirrorEffectCoroutine != null)
			StopCoroutine(mirrorEffectCoroutine);
		if (blurEffectCamera != null)
			blurEffectCamera.enabled = false;
	}

	void IRestartable.Restart()
	{
		if (mirrorEffectCoroutine != null)
			StopCoroutine(mirrorEffectCoroutine);
		isItUsedNow = false;
		blurEffectCamera.enabled = false;
	}
}
