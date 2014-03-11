using UnityEngine;

public class UpgradedShootingEnemy : MonoBehaviour
{
	public float speed = 15;
	public int health = 200;
	public float damage = 2.0f;

	public GenericEnemy self;
	public Transform target;
	public AudioClip laser;

	// A value to give the minimum amount of Torque when dying
	public float deathSpinMin = -100;
	// A value to give the maximum amount of Torque when dying
	public float deathSpinMax = 100;

	/// <summary>
	/// Projectile prefab for shooting
	/// </summary>
	public Transform shotPrefab;

	/// <summary>
	/// Cooldown in seconds between two shots
	/// </summary>
	public float shootingRate = 1.25f;

	//
	// 2  Cooldown
	//
	private float shootCooldown = 0f;
	private SpriteRenderer ren;
	private EnemyDeath death;
	private EnemyMovement move;
	private bool dying = false;

	void Start () // initialization
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

		if (Vector2.Distance (target.transform.position, this.transform.position) < 15)
			return;

		move.Move (this, target, self);
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

		EnemyProjectile projectile = shotTransform.gameObject.GetComponent<EnemyProjectile> ();
		if (projectile != null)
		{
			projectile.oldTarget = new Vector3 (target.position.x, target.position.y);
			projectile.parent = this.gameObject;
		}

		audio.PlayOneShot(laser);
	}

	public bool CanAttack { get { return shootCooldown <= 0f; } }

	private void OnCollisionEnter2D (Collision2D coll)
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
			&& coll.gameObject.GetComponent<EnemyProjectile>().parent != this.gameObject)
		{
			death.Death (this, ren, deathSpinMin, deathSpinMax);
		}
	}
}
