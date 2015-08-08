using UnityEngine;
using System.Collections;
using Enums;

public class DecayGroundEffect : MonoBehaviour, IRestartable {

	public GameObject decayParticleS;
	public GameObject decayParticleM;
	public GameObject decayParticleL;

	// Use this for initialization
	void Start () {
		decayParticleS.SetActive(true);
		decayParticleM.SetActive(false);
		decayParticleL.SetActive(false);
	}

	public void PlayDecayEffect()
	{
		StartCoroutine(PlayDecayEffectCoroutine());
	}

	IEnumerator PlayDecayEffectCoroutine()
	{
		decayParticleS.SetActive(false);
		decayParticleM.SetActive(true);
		yield return new WaitForSeconds(GetComponent<Decay>().delay);
		decayParticleL.SetActive(true);
	}

	void IRestartable.Restart()
	{
		decayParticleS.SetActive(true);
		decayParticleM.SetActive(false);
		decayParticleL.SetActive(false);
	}
}
