using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

public class PrologueScene : MonoBehaviour {
	
	public List<Sprite> images;
	public SpriteRenderer spriteRenderer;
	void Start()
	{
		SetBlack();
		StartCoroutine(ShowCutscenes());	
	}

    private void SetBlack()
    {
        spriteRenderer.color -= new Color(1, 1, 1, 0);
    }

    IEnumerator ShowCutscenes()
	{
		int frameLength = 50;
		Color unit = new Color(0.02f, 0.02f, 0.02f, 0);
		int sceneShowLength = 2;

		foreach (var image in images)
		{
			spriteRenderer.sprite = image;
			
			for (int i=0; i<frameLength; i++)
			{
				spriteRenderer.color += unit;
				yield return null;
			}

			yield return new WaitForSeconds(sceneShowLength);
			
			for (int i=0; i<frameLength; i++)
			{
				spriteRenderer.color -= unit;
				yield return null;
			}
		}
	}
}
