using UnityEngine;
using System.Collections;
using Enums;

public class ActiveTempJail : MonoBehaviour {

	public GameObject tempJail;
	public GameObject shoutZone;
	public GameObject nextShoutZone;
	public IsDark disappearIn;
	
	public GameObject mirror;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		if ((shoutZone == null) && (nextShoutZone != null) && (tempJail.activeInHierarchy == false) 
			&& (disappearIn != Global.ingame.isDark) && (GameObject.FindObjectOfType<TextBoxManager>().isActive == false))
			tempJail.SetActive(true);
		if ((tempJail.activeInHierarchy == true) && (disappearIn == Global.ingame.isDark))
		{
			tempJail.SetActive(false);	
			Destroy(mirror.GetComponent<SwitchDarkLight>(), 1f);
		}
	}
}
