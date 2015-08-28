using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System;

public class TextBoxManagerForNormalEnding : TextBoxManager {
	private bool isStarted = false;
	
	public Image blackImage;
	
	void Awake()
	{
		blackImage.color = new Color(1, 1, 1, 0);
	}
	protected override void Start()
	{
		base.Start();
		isStarted = true;
	}
	public override void DisableTextBox()
	{
		if (!isStarted)
		{
			return;
		}
		base.DisableTextBox();
		Debug.Log("End");

		//Start animation. need to be moved.
		player.canMove = false;
		StartCoroutine(ShowAnimation());
	}

	IEnumerator ShowAnimation()
	{
		yield return new WaitForSeconds(1);
		for (int i=0; i<50; i++)
		{
			blackImage.color += new Color(0, 0, 0, 0.02f);
			yield return null;
		}
		Scene.Load("Title", Scene.SceneType.MainScene);
	}
}
