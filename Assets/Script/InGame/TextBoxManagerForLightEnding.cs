using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System;

public class TextBoxManagerForLightEnding : TextBoxManager {
	private bool isStarted = false;
	
	public Image blackImage;
	public Image nextCutScene;
	
	void Awake()
	{
		nextCutScene.color = new Color(1, 1, 1, 0);
		blackImage.color = new Color(1, 1, 1, 0);
	}
	protected override void Start()
	{
		base.Start();
		isStarted = true;
	}

	private bool isEffectRunning = false;
	protected override void Update()
	{
		if (!isActive) 
		{
			return;
		} 
		
		if (Input.GetKeyDown (KeyCode.Space) && isEffectRunning == false)
		{
			currentLine += 1;
			if (currentLine > endAtLine) 
			{
				DisableTextBox ();
				return;
			}

			theText.text = textLines [currentLine];
			if (IsEffect(textLines [currentLine]))
			{
				theText.text = "";
				StartCoroutine(ShowEffect(textLines [currentLine]));
			}
		}

	}

    private bool IsEffect(string line)
    {
		var normalized = line.ToLower().Trim();
		if (normalized.StartsWith("effect"))
		{
			return true;
		}
		return false;
    }

    IEnumerator ShowEffect(String line)
	{
		var normalized = line.ToLower();
		isEffectRunning = true;

		var effectNum = int.Parse(normalized.Split('_')[1]);
		
		Debug.Log("Effect num is " + effectNum);
		switch (effectNum)
		{
			case 1:
				yield return StartCoroutine(MakeNPercentDark(50, blackImage));
				break;
			case 2:
				yield return StartCoroutine(MakeNPercentDark(30, blackImage));
				theText.color = Color.white;
				break;
			case 3:
				yield return StartCoroutine(MakeNPercentDark(20, blackImage));
				break;
			case 4:
				nextCutScene.color += new Color(0, 0, 0, 1f);
				yield return StartCoroutine(MakeNPercentWhite(50, blackImage));
				break;
			case 5:
				yield return StartCoroutine(MakeNPercentWhite(50, blackImage));
				break;
			case 6:
				yield return StartCoroutine(MakeNPercentDark(100, blackImage));
				break;
			default:
			break;
		}

		yield return null;		
		isEffectRunning = false;
	}

    private IEnumerator MakeNPercentDark(int percent, Image image)
    {
		for (int i=0; i<percent; i++)
		{
			image.color += new Color(0, 0, 0, 0.01f);
			yield return null;
		}
    }

	private IEnumerator MakeNPercentWhite(int percent, Image image)
    {
		for (int i=0; i<percent; i++)
		{
			image.color -= new Color(0, 0, 0, 0.01f);
			yield return null;
		}
    }

    public override void DisableTextBox()
	{
		if (!isStarted)
		{
			return;
		}
		base.DisableTextBox();
		Debug.Log("End");

		Scene.Load("Title", Scene.SceneType.MainScene);
	}
}
