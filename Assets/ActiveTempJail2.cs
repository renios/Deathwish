using UnityEngine;
using System.Collections;
using Enums;

public class ActiveTempJail2 : MonoBehaviour {

	public GameObject tempJail;
	public Collider2D collider;
	public GameObject nextShoutZone;
	public IsDark disappearIn;
	
	public GameObject mirror;

	// Use this for initialization
	void Start () {
		collider = GetComponent<Collider2D>();
	}
	
	// Update is called once per frame
	void Update () {
		if ((collider == null) && (nextShoutZone != null) && (tempJail.activeInHierarchy == false) 
			&& (disappearIn != Global.ingame.isDark))
			tempJail.SetActive(true);
		if ((tempJail.activeInHierarchy == true) && (disappearIn == Global.ingame.isDark))
		{
			tempJail.SetActive(false);	
			Destroy(mirror.GetComponent<SwitchDarkLight>());
		}
	}

	void OnTriggerEnter2D()
	{
		Destroy(collider);
	}
}
