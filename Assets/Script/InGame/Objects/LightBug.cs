using UnityEngine;
using System.Collections;
using System;

public class LightBug : MonoBehaviour {

	public GameObject attachCollider;
	public GameObject awayCollider;

	public GameObject[] movePoints;
	private GameObject currentPoint;
	private float speed = 3;

	public void MoveNextPoint()
	{
		StartCoroutine("MoveNextPointCoroutine");
	}

	IEnumerator MoveNextPointCoroutine()
	{
		awayCollider.GetComponent<Collider2D>().enabled = false;
		Hashtable hash = new Hashtable();
		hash.Add("position", movePoints[GetNextIndex()].transform.position);
		hash.Add("speed", speed);
		hash.Add("easetype", iTween.EaseType.easeOutQuad);
		iTween.MoveTo(gameObject, hash);
		yield return new WaitForSeconds(CalculateTime());
		currentPoint = movePoints[GetNextIndex()];
		awayCollider.GetComponent<Collider2D>().enabled = true;
	}

	float CalculateTime()
	{
		return (currentPoint.transform.position - movePoints[GetNextIndex()].transform.position).magnitude / speed; 
	}

	int GetNextIndex()
	{
		int result = Array.IndexOf(movePoints, currentPoint) + 1;
		if (result > movePoints.GetUpperBound(0)) result = 0;
		return result;
	}

	// Use this for initialization
	void Start () {
		currentPoint = movePoints[movePoints.GetLowerBound(0)];
		gameObject.transform.position = movePoints[movePoints.GetLowerBound(0)].transform.position;
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
