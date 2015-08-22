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

	public enum CharacterAction
	{
		Default,
		Walk,
		Jump,
		Land
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
