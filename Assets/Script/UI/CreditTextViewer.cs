using UnityEngine;
using System.Collections;

public class CreditTextViewer : MonoBehaviour {

	public GameObject[] creditObjects;

	// Use this for initialization
	IEnumerator Start () {
		Initialize();

		yield return new WaitForSeconds(1);

		foreach (GameObject creditObject in creditObjects)
		{
			yield return StartCoroutine(ViewText(creditObject));
		}

		Application.LoadLevel("Title");
	}

	IEnumerator ViewText(GameObject creditObject)
	{
		TextMesh[] textMeshes = creditObject.GetComponentsInChildren<TextMesh>();
		//  SpriteRenderer renderer = creditObject.GetComponentInChildren<SpriteRenderer>();

		for (int i=0; i<50; i++)
		{
			foreach (TextMesh textMesh in textMeshes)
				textMesh.color += new Color(0, 0, 0, 0.02f);
			if (creditObject.GetComponentInChildren<SpriteRenderer>() != null)
				creditObject.GetComponentInChildren<SpriteRenderer>().color += new Color(0, 0, 0, 0.02f);
			yield return new WaitForSeconds(0.005f);
		}

		yield return new WaitForSeconds(2);

		for (int i=0; i<50; i++)
		{
			foreach (TextMesh textMesh in textMeshes)
				textMesh.color -= new Color(0, 0, 0, 0.02f);
			if (creditObject.GetComponentInChildren<SpriteRenderer>() != null)
				creditObject.GetComponentInChildren<SpriteRenderer>().color -= new Color(0, 0, 0, 0.02f);
			yield return new WaitForSeconds(0.005f);
		}

		yield return new WaitForSeconds(1);
	}

	// Update is called once per frame
	void Initialize () {
		foreach (GameObject creditObject in creditObjects)
		{
			TextMesh[] textMeshes = creditObject.GetComponentsInChildren<TextMesh>();
			foreach (TextMesh textMesh in textMeshes)
			{
				textMesh.color -= new Color(0, 0, 0, 1);
			}
			if (creditObject.GetComponentInChildren<SpriteRenderer>() != null)
				creditObject.GetComponentInChildren<SpriteRenderer>().color -= new Color(0, 0, 0, 1);
		}
	}
}
