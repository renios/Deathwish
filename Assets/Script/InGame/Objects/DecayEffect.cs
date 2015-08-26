using UnityEngine;
using System.Collections;
using Enums;

public class DecayEffect : MonoBehaviour, IRestartable {

	public GameObject permanentDecayParticle;
	public GameObject decayingParticle;
	public GameObject afterDecayParticle;

	// Use this for initialization
	void Start () {
		permanentDecayParticle.SetActive(true);
		decayingParticle.SetActive(false);
		afterDecayParticle.SetActive(false);
	}

	public void PlayDecayEffect()
	{
		StartCoroutine(PlayDecayEffectCoroutine());
	}

	IEnumerator PlayDecayEffectCoroutine()
	{
		permanentDecayParticle.SetActive(false);
		permanentDecayParticle.GetComponent<ParticleSystem> ().Stop ();
		permanentDecayParticle.GetComponent<ParticleSystem> ().loop = false;
		decayingParticle.SetActive(true);
		yield return new WaitForSeconds(GetComponent<Decay>().delay);
		afterDecayParticle.SetActive(true);
	}

	void IRestartable.Restart()
	{
		permanentDecayParticle.SetActive(true);
		permanentDecayParticle.GetComponent<ParticleSystem> ().loop = true;
		permanentDecayParticle.GetComponent<ParticleSystem> ().Play ();
		decayingParticle.SetActive(false);
		afterDecayParticle.SetActive(false);
	}
}
