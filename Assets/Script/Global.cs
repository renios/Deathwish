using UnityEngine;
using System.Collections;

public class Global
{
	public static WorldControl world;

	//FIXME: when start from stage scene, scene class is not initialized.
	// intiialize before use global value.
	static Global()
	{
		// Log currentSceneType for static constructor is called.
		Debug.Log ("Start Scene " + Scene.currentSceneType.ToString());
	}
}
