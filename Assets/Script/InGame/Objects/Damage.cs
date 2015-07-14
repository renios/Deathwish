using UnityEngine;
using System.Collections;

public class Damage : MonoBehaviour
{
	public float timeBetweenAttacks = 1f;
	public int attackDamage = 10;

	GameObject player;
	PlayerHealth playerHealth;
	public bool playerInRange;
	float timer;
	
	
	void Awake ()
	{
		playerInRange = false;
		player = GameObject.FindGameObjectWithTag ("Player");
		playerHealth = player.GetComponent <PlayerHealth> ();
	}
	
	
	void OnCollisionEnter2D (Collision2D coll)
	{
		if(coll.gameObject == player)
		{
			playerInRange = true;
		}
	}
	
	
	void OnCollisionExit2D (Collision2D coll)
	{
		if(coll.gameObject == player)
		{
			playerInRange = false;
		}
	}
	
	
	void Update ()
	{
		timer += Time.deltaTime;
		
		if(timer >= timeBetweenAttacks && playerInRange)
		{
			Attack ();
		}
		
		if(playerHealth.currentHealth <= 0)
		{
			Restarter.RestartAll();
		}
	}
	
	
	void Attack ()
	{
		timer = 0f;
		
		if(playerHealth.currentHealth > 0)
		{
			playerHealth.TakeDamage (attackDamage);
		}
	}
}