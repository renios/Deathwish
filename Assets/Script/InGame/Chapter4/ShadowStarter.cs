using UnityEngine;
using System.Collections;
using System;

public class ShadowStarter : MonoBehaviour, IRestartable {

	// Use this for initialization
	void Awake () {
		Global.ingame.isDark = Enums.IsDark.Dark;
	}

    void IRestartable.Restart()
    {
		Global.ingame.isDark = Enums.IsDark.Dark;
    }
}
