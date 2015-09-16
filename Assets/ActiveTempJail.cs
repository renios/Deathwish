using UnityEngine;
using System.Collections;
using Enums;

public class ActiveTempJail : MonoBehaviour {

	public enum ActiveTrigger
	{
		None,
		ShoutZone,
		Collider
	}

	public ActiveTrigger activeTrigger;
	public GameObject tempJail;
	public GameObject triggerShoutZone;
	public Collider2D triggerCollider;
	public GameObject nextShoutZone;
	public IsDark disappearIn;
	
	public GameObject triggerMirror;

	// Use this for initialization
	void Start () {

	}
	
	// Update is called once per frame
	void Update () {
		if ((activeTrigger == ActiveTrigger.ShoutZone) && (triggerShoutZone == null) && (GameObject.FindObjectOfType<TextBoxManager>().isActive == false)
			&& (nextShoutZone != null) && (tempJail.activeInHierarchy == false) 
			&& (disappearIn != Global.ingame.isDark))
			tempJail.SetActive(true);
		else if ((activeTrigger == ActiveTrigger.Collider) && (triggerCollider == null)
			&& (nextShoutZone != null) && (tempJail.activeInHierarchy == false) 
			&& (disappearIn != Global.ingame.isDark))
			tempJail.SetActive(true);

		if ((tempJail.activeInHierarchy == true) && (disappearIn == Global.ingame.isDark))
		{
			tempJail.SetActive(false);	
			Destroy(triggerMirror.GetComponent<SwitchDarkLight>(), 1f);
		}
	}

	void OnTriggerEnter2D()
	{
		if (activeTrigger == ActiveTrigger.Collider)
			Destroy(triggerCollider);
	}
}
