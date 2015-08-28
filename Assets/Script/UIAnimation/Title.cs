using UnityEngine;
using System.Collections;
using System;

public class Title : MonoBehaviour {
	public SpriteRenderer deathImageRenderer;
	public bool cheatOn = false;

	void Start () {
		deathImageRenderer.color -= new Color(1,1,1,0);
		StartCoroutine(LoadMenu());
	}

    private IEnumerator LoadMenu()
    {
		var spriteRenderer = GetComponent<SpriteRenderer>();

		spriteRenderer.color -= new Color(1,1,1,0);

		for (int i=0; i<50; i++)
		{
			spriteRenderer.color += new Color(0.02f, 0.02f, 0.02f, 0);
			yield return null;
		}
		
		if (SaveLoad.IsAllCleared() || cheatOn)
		{
			for (int i=0; i<50; i++)
			{
				deathImageRenderer.color += new Color(0.02f, 0.02f, 0.02f, 0);
				yield return null;
			}
		}

		yield return new WaitForSeconds(1.0f);

		for (int i=0; i<50; i++)
		{
			spriteRenderer.color -= new Color(0.02f, 0.02f, 0.02f, 0);
			deathImageRenderer.color -= new Color(0.02f, 0.02f, 0.02f, 0);
			yield return null;
		}

        Scene.Load("Menu", Scene.SceneType.MainScene);
    }
}
