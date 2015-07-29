using UnityEngine;
using System.Collections;

public class LightBug : MonoBehaviour {

	public GameObject lightCollider;
	public GameObject awayCollider;

	public GameObject[] MovePoints;

	public void MoveNextPoint()
	{
		StartCoroutine("MoveNextPointCoroutine");
	}

	IEnumerator MoveNextPointCoroutine()
	{
		awayCollider.GetComponent<Collider2D>().enabled = false;
		iTween.MoveTo(gameObject, gameObject.transform.position + new Vector3(10, 0, 0), 1);
		yield return new WaitForSeconds(1);
		awayCollider.GetComponent<Collider2D>().enabled = true;
	}

	// Use this for initialization
	void Start () {

	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
