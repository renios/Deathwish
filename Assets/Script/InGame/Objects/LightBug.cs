using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;
using Enums;

public class LightBug : MonoBehaviour, IRestartable {

	public GameObject attachCollider;
	public GameObject awayCollider;

	public GameObject[] movePoints;
	private GameObject currentPoint;

	private List<Func<Vector3>> movePointGetters;
	private float speed = 3;
	private bool isMoving = false;

	// Use this for initialization
	void Start () {
		currentPoint = movePoints[movePoints.GetLowerBound(0)];
		gameObject.transform.position = movePoints[movePoints.GetLowerBound(0)].transform.position;

		GetComponentInChildren<AwayFromCharacterCollider>().SetReceive(this);
		GetComponentInChildren<AttachToAreaMarker>().SetParentObjectType(ObjectType.LightBug);
	}

	void Update()
	{
		if ((GetComponent<LightState>().GetLightState() == LightState.IsLight.True) && (!isMoving))
			GetComponentInChildren<AwayFromCharacterCollider>().gameObject.GetComponent<Collider2D>().enabled = true;
		else
			GetComponentInChildren<AwayFromCharacterCollider>().gameObject.GetComponent<Collider2D>().enabled = false;
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
		currentPoint = movePoints[movePoints.GetLowerBound(0)];
		gameObject.transform.position = movePoints[movePoints.GetLowerBound(0)].transform.position;
		GetComponentInChildren<AwayFromCharacterCollider>().gameObject.GetComponent<Collider2D>().enabled = true;
		gameObject.GetComponent<SpriteRenderer>().sprite = gameObject.GetComponent<SpriteSwitch>().light;		
	}
}
