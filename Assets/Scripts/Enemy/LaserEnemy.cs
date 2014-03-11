using UnityEngine;
using System.Collections;

public class LaserEnemy : MonoBehaviour 
{
	public float speed = 5;
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
	public Transform shotPrefab;

	/// <summary>
	/// Cooldown in seconds between two shots
	/// </summary>
	public float shootingRate = 1;

	//
	// 2  Cooldown
	//
	private float shootCooldown = 0f;
	private SpriteRenderer ren;
	private EnemyDeath death;
	private EnemyMovement move;
	private EnemyShoot shoot;
	private bool dying = false;

	// Use this for initialization
	void Start () 
	{
		self = new GenericEnemy (this.gameObject, health, speed, damage);
		ren = transform.Find ("body").GetComponent<SpriteRenderer> ();

		if (!target)
			target = GameObject.FindWithTag ("Player").transform;

		death = this.GetComponent<EnemyDeath> ();
		move = this.GetComponent<EnemyMovement> ();
		move.transform = this.transform;
		move.self = this.self;
		shoot = this.GetComponent<EnemyShoot> ();
		shoot.laser = this.laser;
		shoot.shotPrefab = this.shotPrefab;
		shoot.shootingRate = this.shootingRate;
	}

	void Update ()
	{
		if (dying)
			return;

		shoot.TryShoot (target);
	}

	void FixedUpdate ()
	{
		if (dying)
			return;

		if (self.health <= 0)
			death.Death (this, ren, deathSpinMin, deathSpinMax);

		move.Move (target);
	}

	void OnCollisionEnter2D (Collision2D coll)
	{
		if (coll.gameObject.tag == "Enemy")
		{
			if (coll.gameObject.name == "Enemy 2")
				death.Death (this, ren, deathSpinMin, deathSpinMax);
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
