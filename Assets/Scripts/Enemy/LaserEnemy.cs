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
	}

	void Update ()
	{
		if (dying)
			return;

		if (shootCooldown > 0)
		{
			shootCooldown -= Time.deltaTime;
		}
		else
		{
			Shoot (false);
		}
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

	private void Shoot (bool isEnemy)
	{
		if (!CanAttack)
			return;

		shootCooldown = shootingRate;

		// Create a new shot
		var shotTransform = Instantiate (shotPrefab) as Transform;

		// Assign position
		shotTransform.position = transform.position;

		var projectile = shotTransform.gameObject.GetComponent<EnemyProjectile> ();
		if (projectile != null)
		{
			projectile.oldTarget = new Vector3 (target.position.x, target.position.y);
			projectile.parent = this.gameObject;
		}

		// TODO: audio.PlayOneShot (laser);
	}

	public bool CanAttack { get { return shootCooldown <= 0f; } }
}
