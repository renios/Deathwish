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
		
	}

	void Initialize()
	{
		chapterBackground.GetComponent<SpriteRenderer>().color = Color.black;
		chapterPrologueText.GetComponent<TextMesh>().color += new Color(0, 0, 0, -1); 
		chapterTitleText.GetComponent<TextMesh>().color += new Color(0, 0, 0, -1);
	}

	IEnumerator FadeInBackground()
	{
		SpriteRenderer sr = chapterBackground.GetComponent<SpriteRenderer>(); 
		for (int i=0; i<100; i+=2)
		{
			sr.color += new Color(0.02f, 0.02f, 0.02f, 0);
			yield return new WaitForSeconds(0.005f);
		}
	}

	// Update is called once per frame
	void Update () {
	
	}
}
