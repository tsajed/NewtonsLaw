using UnityEngine;
using System.Collections;

public class GenericEnemy {

	public GameObject self;

	private int health = 100;
	private float movementSpeed = 2.0f;
	private float damage = 2.0f;


	public GenericEnemy(GameObject self, int health, float speed, float damage) {
		this.self = self;
		this.health = health;
		this.movementSpeed = speed;
		this.damage = damage;
	}

	// Flips the Sprite so it looks in the opposite direction
	public void Flip() {
		// Multiply the x component of localScale by -1.
		Vector3 enemyScale = self.transform.localScale;
		enemyScale.x *= -1;
		self.transform.localScale = enemyScale;
	}

	public float getSpeed() {
		return movementSpeed;
	}

	public float getDamage() {
		return damage;
	}

	public int getHealth() {
		return health;
	}

	public void increaseHealth(int amount) {
		health += amount;
	}

	public void decreaseHealth(int amount) {
		health -= amount;
	}

	public void setDamage(float newDamage) {
		damage = newDamage;
	}

	public void setHealth(int newHealth) {
		health = newHealth;
	}

	public void setSpeed(float newSpeed) {
		movementSpeed = newSpeed;
	}
}
