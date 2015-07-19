using UnityEngine;
using System.Collections;

public class Damage : MonoBehaviour
{
	void OnCollisionEnter2D (Collision2D collision)
	{
		Restarter.RestartAll();
	}
}