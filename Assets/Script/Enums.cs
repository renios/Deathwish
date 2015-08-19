using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace Enums
{
	public enum CharacterLocation
	{
		OnBlock,
		OnAir,
		OnLadder
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
}
