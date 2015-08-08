using UnityEngine;
using UnityEngine.Assertions;
using System.Collections;
using System;
using Enums;

public class Dust : MonoBehaviour, IRestartable {

	public GameObject attachCollider;
	public GameObject awayCollider;
	public GameObject[] movePoints;

	private GameObject currentPoint;
	private float speed = 3;
	private bool isMoving = false;

	// Use this for initialization
	void Start () {
		Assert.IsFalse(movePoints.Length == 0, "MovePoints of firefly is empty");
		foreach (GameObject movePoint in movePoints)
			Assert.IsNotNull(movePoint, "Some movePoint slot of firefly are empty");

		currentPoint = movePoints[0];
		gameObject.transform.position = movePoints[0].transform.position;

		GetComponentInChildren<PlayerDetector>().SetCallBack(MoveNextPoint);
		GetComponentInChildren<AttachToAreaMarker>().SetParentObjectType(ObjectType.Dust);
	}

	void Update()
	{
		if ((Global.ingame.isDark == IsDark.Light) && (!isMoving))
			GetComponentInChildren<PlayerDetector>().gameObject.GetComponent<Collider2D>().enabled = true;
		else
			GetComponentInChildren<PlayerDetector>().gameObject.GetComponent<Collider2D>().enabled = false;
	}

	public void MoveNextPoint()
	{
		StartCoroutine(MoveNextPointCoroutine());
	}

	IEnumerator MoveNextPointCoroutine()
	{
		isMoving = true;
		Hashtable hash = new Hashtable();
		hash.Add("position", movePoints[GetNextIndex()].transform.position);
		hash.Add("speed", speed);
		hash.Add("easetype", iTween.EaseType.easeOutQuad);
		iTween.MoveTo(gameObject, hash);
		yield return new WaitForSeconds(CalculateTime());
		currentPoint = movePoints[GetNextIndex()];
		isMoving = false;
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

	void IRestartable.Restart()
	{
		StopAllCoroutines();
		iTween.Stop(gameObject);
		isMoving = false;
		currentPoint = movePoints[0];
		gameObject.transform.position = movePoints[0].transform.position;
		GetComponentInChildren<PlayerDetector>().gameObject.GetComponent<Collider2D>().enabled = true;
		gameObject.GetComponent<SpriteRenderer>().sprite = gameObject.GetComponent<SpriteSwitch>().light;		
	}
}
