using UnityEngine;
using System.Collections;

public class SpikeEnemy : MonoBehaviour 
{
	public float speed = 25;
	public int health = 100;
	public float damage = 2.0f;

	public GenericEnemy self;
	public Transform target;

	// A value to give the minimum amount of Torque when dying
	public float deathSpinMin = -100f;
	// A value to give the maximum amount of Torque when dying
	public float deathSpinMax = 50f;

	private bool dying = false;
	private SpriteRenderer ren;
	private EnemyDeath death;
	private EnemyMovement move;

	void Start () 
	{
		self = new GenericEnemy (this.gameObject, health, speed, damage);
		ren = transform.Find ("body").GetComponent<SpriteRenderer> ();

		if (!target)
			target = GameObject.FindWithTag ("Player").transform;

		death = this.GetComponent<EnemyDeath> ();
		move = this.GetComponent<EnemyMovement> ();
	}

	void FixedUpdate ()
	{
		if (dying)
			return;

		if (self.health <= 0)
			death.Death (this, ren, deathSpinMin, deathSpinMax);

		move.Move (this, target, self);
	}

	void OnCollisionEnter2D (Collision2D coll)
	{
		if (coll.gameObject.tag == "Enemy")
		{
			// do nothing
		}
		else if (coll.gameObject.tag == "Player")
		{
			// hurt player
			self.decreasePlayerHealth(1);
		}
		else if (coll.gameObject.tag == "Bullet"
			&& coll.gameObject.GetComponent<EnemyProjectile> ().parent != this.gameObject)
		{
			death.Death (this, ren, deathSpinMin, deathSpinMax);
		}
	}
}
