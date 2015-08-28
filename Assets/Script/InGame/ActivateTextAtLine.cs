using UnityEngine;
using System.Collections;
using Enums;

public class ActivateTextAtLine : MonoBehaviour {

	public TextAsset theText;

	private int startLine = 0;
	private int endLine;

	public TextBoxManager theTextBox;

	public bool requireButtonPress;
	private bool waitForPress;

	public bool destroyWhenActivated;

	public bool LightMode;
	public bool DarkMode;

	// Use this for initialization
	void Start () 
	{
		theTextBox = FindObjectOfType<TextBoxManager> ();

	}
	
	// Update is called once per frame
	void Update () 
	{
		if (waitForPress && Input.GetKeyDown (KeyCode.J)) {
			theTextBox.ReloadScript (theText);
			theTextBox.currentLine = startLine;
			theTextBox.endAtLine = endLine;
			theTextBox.EnableTextBox ();
			
			if (destroyWhenActivated) {
				Destroy (gameObject);
			}
		}
	}

	void OnTriggerStay2D(Collider2D other)
	{
		if (IsItDark() && DarkMode || !IsItDark() && LightMode) 
		{

			if (other.name == "Player") {
				if (requireButtonPress) {
					waitForPress = true;
					return;
				}
				theTextBox.ReloadScript (theText);
				theTextBox.currentLine = startLine;
				theTextBox.endAtLine = theTextBox.textLines.Length - 1;
				theTextBox.EnableTextBox ();

				if (destroyWhenActivated) {
					Destroy (gameObject);
				}
			}
		}
	}
	void OnTriggerExit2D(Collider2D other)
	{
		if (other.name == "Player") 
		{
			waitForPress = false;
		}
	}
	bool IsItDark()
	{
		IsDark isItDark = Global.ingame.GetIsDarkInPosition (gameObject);
		if (isItDark == IsDark.Light) 
		{
			return false;
		} 
		else if (isItDark == IsDark.Dark) 
		{
			return true;
		}
		else 
		{
			return true;
		}
	}
}
