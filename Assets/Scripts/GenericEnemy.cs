using UnityEngine;
using System.Collections;

public class GenericEnemy 
{
	public GameObject self;

	public GameObject target;
	public Health playerHealth;

	public int health { get; set; }
	public int score { get; set; }
	public float movementSpeed { get; set; }
	public float damage { get; set; }
	public float maxSpeed = 5f;				// The fastest the player can travel in the x axis.

	public GenericEnemy(GameObject self, int health, float speed, float damage, int score=100) 
	{
		this.self = self;
		this.health = health;
		this.movementSpeed = speed;
		this.damage = damage;
		this.score = score;
		target = GameObject.FindWithTag("Player");
		playerHealth = target.GetComponent<Health>();
	}

	// Flips the Sprite so it looks in the opposite direction
	public void Flip()
	{
		// Multiply the x component of localScale by -1.
		Vector3 enemyScale = self.transform.localScale;
		enemyScale.x *= -1;
		self.transform.localScale = enemyScale;
	}

	public void increaseHealth(int amount) { health += amount; }

	public void decreaseHealth(int amount) { health -= amount; }

	public void decreasePlayerHealth(int amount) { 	playerHealth.decreasePlayerHP(amount); }
}
