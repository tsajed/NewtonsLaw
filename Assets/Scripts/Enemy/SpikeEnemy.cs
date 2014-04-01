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
	private EnemyScore score;

	void Start () 
	{
		self = new GenericEnemy (this.gameObject, health, speed, damage);
		ren = transform.Find ("body").GetComponent<SpriteRenderer> ();

		if (!target) { target = GameObject.FindWithTag ("Player").transform; }

		death = this.GetComponent<EnemyDeath> ();
		death.die = this;
		move = this.GetComponent<EnemyMovement> ();
		move.location = this.transform;
		move.self = this.self;
		score = this.GetComponent<EnemyScore> ();
		score.self = this.self;
	}

	void FixedUpdate ()
	{
		if (dying) { return; }

		if (self.health <= 0) { Death (); }

		move.TryMove (target);
	}

	void OnCollisionEnter2D (Collision2D coll)
	{
		if (dying) { return; }

		if (coll.gameObject.tag == "Enemy")
		{
			// do nothing
		}
		else if (coll.gameObject.tag == "Player")
		{
			// hurt player
			self.decreasePlayerHealth(1);
		}
		else if (coll.gameObject.tag == "Bullet")
		{
			Death ();
		}
	}

	private void Death ()
	{
		dying = true;
		score.createScore ();
		death.Death (ren, deathSpinMin, deathSpinMax);
	}
}
