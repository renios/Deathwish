using UnityEngine;
using System.Collections;

public class LightState : MonoBehaviour {
	
	public enum IsLight
	{
		True,
		False
	}

	public enum AttachedLightBug
	{
		False,
		True
	}

	private IsLight lightState;
	private AttachedLightBug attachedLightBug;

	// Use this for initialization
	void Start () {
		InitiateToLight();
	}
	
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

	public AttachedLightBug IsAttachedLightBug()
	{
		return attachedLightBug;
	}

	public void AttachLightBug()
	{
		attachedLightBug = AttachedLightBug.True;
	}

	public void DetachLightBug()
	{
		attachedLightBug = AttachedLightBug.False;
	}

	void InitiateToLight()
	{
		lightState = IsLight.True;
		attachedLightBug = AttachedLightBug.False;
	}
}
