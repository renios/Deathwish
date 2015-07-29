using UnityEngine;
using System.Collections;

public class LightState : MonoBehaviour {
	
	public enum IsLight
	{
		True,
		False
	}

	private IsLight lightState;
	
	public IsLight GetLightState()
	{
		return lightState; 
	}

	public void ChangeStateToLight()
	{
		lightState = IsLight.True;
	}
	
	public void ChangeStateToDark()
	{
		lightState = IsLight.False;
	}

	void InitiateToLight()
	{
		lightState = IsLight.True;
	}

	// Use this for initialization
	void Start () {
		InitiateToLight();
	}
}
