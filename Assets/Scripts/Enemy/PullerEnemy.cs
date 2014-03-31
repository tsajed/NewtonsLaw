using UnityEngine;
using System.Collections;


public class PullerEnemy : MonoBehaviour 
{
	public float speed = 10;
	public int health = 100;
	public float damage = 2.0f;

	public GenericEnemy self;
	public Transform target;
	public AudioClip laser;

	// A value to give the minimum amount of Torque when dying
	public float deathSpinMin = -100f;
	// A value to give the maximum amount of Torque when dying
	public float deathSpinMax = 50f;

	/// <summary>
	/// Projectile prefab for shooting
	/// </summary>
	public GameObject[] shotPrefab;

	/// <summary>
	/// Cooldown in seconds between two shots
	/// </summary>
	public float shootingRate = 3;

	private SpriteRenderer ren;
	private EnemyDeath death;
	private EnemyMovement move;
	private EnemyShoot shoot;
	private bool dying = false;

	void Start () // Use this for initialization
	{
		self = new GenericEnemy (this.gameObject, health, speed, damage);
		ren = transform.Find ("body").GetComponent<SpriteRenderer> ();

		if (!target) { target = GameObject.FindWithTag ("Player").transform; }

		death = this.GetComponent<EnemyDeath> ();
		death.die = this;
		move = this.GetComponent<EnemyMovement> ();
		move.location = this.transform;
		move.self = this.self;
		shoot = this.GetComponent<EnemyShoot> ();
		shoot.sound = this.laser;
		shoot.shotPrefab = this.shotPrefab;
		shoot.shootingRate = this.shootingRate;
	}

	void Update ()
	{
		if (dying) { return; }

		if (!shoot.child) { shoot.TryShoot (target, 0); }
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
			if (coll.gameObject.name.Contains ("Enemy 2") ||
				coll.gameObject.name.Contains ("Enemy Mito"))
			{ Death (); }
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
		death.Death (ren, deathSpinMin, deathSpinMax);
	}
}
