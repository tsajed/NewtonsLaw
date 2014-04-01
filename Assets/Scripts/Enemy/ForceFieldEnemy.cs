using UnityEngine;
using System.Collections;


public class ForceFieldEnemy : MonoBehaviour 
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

	public Sprite dropShield;
	/// <summary>
	/// Cooldown in seconds between two shots
	/// </summary>
	public float shieldsUpRate = 10;
	public float shieldsDownRate = 3;
	public bool shieldsDown { get; private set; }

	//
	// 2  Cooldown
	//
	private float shootCooldown = 0f;

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

		shieldsDown = false;
	}

	void Update ()
	{
		if (dying) { return; }
		
		if (shootCooldown > 0)
		{
			shootCooldown -= Time.deltaTime;
		}
		else
		{
			shootCooldown = shieldsDown ? shieldsUpRate : shieldsDownRate;
			Sprite temp = ren.sprite;
			ren.sprite = dropShield;
			dropShield = temp;
			shieldsDown = !shieldsDown;
		}
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
			if (!shieldsDown) // move projectile away
			{
				var explosionStrength = 1000;
				Vector2 forceVec = -coll.rigidbody.velocity.normalized * explosionStrength;
				coll.rigidbody.AddForce (forceVec);
			}
			else
			{
				Death ();
			}
		}
	}

	private void Death ()
	{
		dying = true;
		score.createScore ();
		death.Death (ren, deathSpinMin, deathSpinMax);
	}
}
