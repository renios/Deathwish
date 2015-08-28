using UnityEngine;
using System.Collections;

public class ChapterStartTextViewer : MonoBehaviour {

	public GameObject chapterTitleText;
	public GameObject chapterPrologueText;
	public GameObject chapterBackground;

	// Use this for initialization
	IEnumerator Start () {
		Initialize();
		
		yield return StartCoroutine(FadeInBackground());

		GameObject.FindObjectOfType<Player>().canMove = false;

		yield return StartCoroutine(ViewText(chapterTitleText.GetComponent<TextMesh>()));
		
		if (chapterPrologueText != null)
			yield return StartCoroutine(ViewText(chapterPrologueText.GetComponent<TextMesh>()));

		yield return StartCoroutine(FadeOutBackground());

		GameObject.FindObjectOfType<Player>().canMove = true;
	}

	void Initialize()
	{
		chapterBackground.GetComponent<SpriteRenderer>().color = Color.black;
		if (chapterPrologueText != null)
			chapterPrologueText.GetComponent<TextMesh>().color += new Color(0, 0, 0, -1); 
		chapterTitleText.GetComponent<TextMesh>().color += new Color(0, 0, 0, -1);
	}

	IEnumerator FadeInBackground()
	{
		SpriteRenderer sr = chapterBackground.GetComponent<SpriteRenderer>(); 
		for (int i=0; i<50; i++)
		{
			sr.color += new Color(0.02f, 0.02f, 0.02f, 0);
			yield return new WaitForSeconds(0.005f);
		}
		
		yield return new WaitForSeconds(1);
	}

	IEnumerator FadeOutBackground()
	{
		SpriteRenderer sr = chapterBackground.GetComponent<SpriteRenderer>(); 
		for (int i=0; i<50; i++)
		{
			sr.color -= new Color(0, 0, 0, 0.02f);
			yield return new WaitForSeconds(0.005f);
		}
	}	
	
	IEnumerator ViewText(TextMesh textMesh)
	{
		for (int i=0; i<50; i++)
		{
			textMesh.color += new Color(0, 0, 0, 0.02f);
			yield return new WaitForSeconds(0.005f);
		}

		yield return new WaitForSeconds(2);

		for (int i=0; i<50; i++)
		{
			textMesh.color -= new Color(0, 0, 0, 0.02f);
			yield return new WaitForSeconds(0.005f);
		}

		yield return new WaitForSeconds(2);
	}

	// Update is called once per frame
	void Update () {
	
	}
}
