using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class MenuDeathwishTitle : MonoBehaviour {
	void Awake () {
		var deathwishTitle = GetComponent<Text>();
		if (SaveLoad.IsAllCleared())
		{
			deathwishTitle.text = "Deathwish";
		}
		else
		{
			deathwishTitle.text = "Wish";
		}
	}
}
