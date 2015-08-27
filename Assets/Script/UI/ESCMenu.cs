using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;

public class ESCMenu : MonoBehaviour {

	public GameObject PopupMenu;
	public GameObject MainMenuButton;

	void Awake () {
		PopupMenu.SetActive(false);
	}
	
	// Update is called once per frame
	void Update () {
		if ((Input.GetKeyDown("escape")) && (PopupMenu.activeInHierarchy == false))
		{
			PopupMenu.SetActive(true);
			EventSystem.current.SetSelectedGameObject(MainMenuButton, null);
		}
		else if ((Input.GetKeyDown("escape")) && (PopupMenu.activeInHierarchy == true))
		{
			PopupMenu.SetActive(false);
			EventSystem.current.SetSelectedGameObject(null);
		}
	}
	
	public void ClosePopup()
	{
		if (PopupMenu.activeInHierarchy == true)
		{
			PopupMenu.SetActive(false);
			EventSystem.current.SetSelectedGameObject(null);
		}
	}
}
