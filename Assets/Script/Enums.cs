using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace Enums
{
	public enum GravityDirection
	{
		Normal,
		Reverse
	}

	public enum SoundType
	{
		None,
		Walk,
		Jump,
		Land,
		GrassPassing,
		Swim,
		BoxPush,
		FireDeath,
		SpikeDeath,
		DustDeath,
		OpenDoor,
		Mirror,
		Decay,
		BoxFalling,
		Lightning,
		FireIsClose
	}

	//FIXME : need better name
	public enum IsDark
	{
		Light,
		Dark
	}
	
	public enum ObjectType
	{
		FireFly,
		Dust
	}

	public enum LampProperty
	{
		LightLamp,
		DarkLamp
	}

	public enum Direction
	{
		Horizontal,
		Vertical
	}

	public enum WindDirection
	{
		Right,
		Left,
		Up,
		Down
	}
}
