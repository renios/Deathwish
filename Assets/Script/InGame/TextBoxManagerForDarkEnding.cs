using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System;

public class TextBoxManagerForDarkEnding : TextBoxManager {
	private bool isStarted = false;
	
	//  public Image blackImage;
	//  public Image nextCutScene;
	public GameObject blackImage;
	public GameObject nextCutScene;
	
	void Awake()
	{
		//  nextCutScene.color = new Color(1, 1, 1, 0);
		//  blackImage.color = new Color(1, 1, 1, 0);
		nextCutScene.GetComponent<SpriteRenderer>().color = new Color(1, 1, 1, 0);
		blackImage.GetComponent<SpriteRenderer>().color = new Color(1, 1, 1, 0);
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
				yield return StartCoroutine(GoToDeath());
				theText.color = Color.white;
				break;
			default:
			break;
		}

		yield return null;		
		isEffectRunning = false;
	}

    private IEnumerator GoToDeath()
    {
        for (int i=0; i<100; i++)
		{
			Global.ingame.GetPlayer().MoveRight1Frame();
			//  blackImage.color += new Color(0, 0, 0, 0.01f);
			blackImage.GetComponent<SpriteRenderer>().color += new Color(0, 0, 0, 0.01f);
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

		StartCoroutine(ShowCutScene());
		
	}

    private IEnumerator ShowCutScene()
    {
		for (int i=0; i<100; i++)
		{
			blackImage.GetComponent<SpriteRenderer>().color += new Color(0, 0, 0, 0.01f);
			yield return null;
		}

		for (int i=0; i<100; i++)
		{
			nextCutScene.GetComponent<SpriteRenderer>().color += new Color(0, 0, 0, 0.01f);
			yield return null;
		}

		yield return new WaitForSeconds(2.0f);

		Scene.Load("Title", Scene.SceneType.MainScene);
    }
}
