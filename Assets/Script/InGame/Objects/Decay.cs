using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Enums;

public class Decay : ObjectMonoBehaviour, IRestartable
{
	public float delay;
	public Sprite normal;
	public Sprite transparent;
	private List<Collider2D> collider2Ds;
	new private SpriteRenderer renderer;
	private SpriteSwitch spriteSwitch;
	public DecayEffect decayEffect;
	bool isDestroy = false;
	IEnumerator destroyingCoroutine;

	public override void StartByParent()
	{
		collider2Ds = new List<Collider2D>(GetComponents<Collider2D> ());
		renderer = GetComponent<SpriteRenderer> ();
		spriteSwitch = GetComponent<SpriteSwitch> ();
		decayEffect = GetComponent<DecayEffect> ();
	}

	public override void UpdateByParent()
	{
		if (!isDestroy)
		{
			foreach (var collider in collider2Ds) {
				collider.enabled = true;
			}
			renderer.enabled = true;
			 
			if (decayEffect != null)
			{
				if(isDestroy) return;

				if (isDarkAfterLamp() == IsDark.Light)
					decayEffect.permanentDecayParticle.SetActive(true);
				else if (isDarkAfterLamp() == IsDark.Dark)
					decayEffect.permanentDecayParticle.SetActive(false);
			}
		}
	}
	
	public override void HideObject()
	{
		GetComponent<SpriteRenderer>().enabled = false;
		foreach (var collider in collider2Ds) {
				collider.enabled = false;
		}

		if (decayEffect != null)
			decayEffect.permanentDecayParticle.SetActive(false);
	}

	void OnCollisionEnter2D(Collision2D collision)
	{
		if((collision.gameObject.tag == "Player" || collision.gameObject.tag == "Box" || collision.gameObject.tag == "Lamp")
					   && isDarkAfterLamp() == Enums.IsDark.Light)
		{
			destroyingCoroutine = DestroySelf(delay);
			StartCoroutine(destroyingCoroutine);

			if (decayEffect != null)
				decayEffect.PlayDecayEffect();
		}
	}

	IEnumerator DestroySelf(float delay)
	{
		yield return new WaitForSeconds(delay);
		SoundEffectController soundEffectController
			= GameObject.FindObjectOfType (typeof(SoundEffectController)) as SoundEffectController;
		soundEffectController.Play (SoundType.Decay);
		isDestroy = true;
		foreach (var collider in collider2Ds) {
			collider.enabled = false;
		}
		renderer.sprite = transparent;
		spriteSwitch.enabled = false;

		destroyingCoroutine = null;
	}

	void IRestartable.Restart()
	{
		if (destroyingCoroutine != null) {
			StopCoroutine(destroyingCoroutine);
		}
		isDestroy = false;
		foreach (var collider in collider2Ds) {
			collider.enabled = true;
		}
		renderer.sprite = normal;
		spriteSwitch.enabled = true;
	}
}
