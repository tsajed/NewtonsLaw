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

	private SpriteRenderer ren;
	private EnemyDeath death;
	private EnemyMovement move;
	private EnemyShoot shoot;
	private bool dying = false;

	void Start () // initialization
	{
		self = new GenericEnemy (this.gameObject, health, speed, damage);
		ren = transform.Find ("body").GetComponent<SpriteRenderer> ();

		if (!target) 
			target = GameObject.FindWithTag ("Player").transform;

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
		if (dying)
			return;

		shoot.TryShoot (target);
	}

	void FixedUpdate ()
	{
		if (dying)
			return;

		if (self.health <= 0)
			Death ();

		if (Vector2.Distance (target.transform.position, this.transform.position) < 15)
			return;

		move.TryMove (target);
	}

	private void OnCollisionEnter2D (Collision2D coll)
	{
		if (coll.gameObject.tag == "Enemy")
		{
			if (coll.gameObject.name.Contains ("Enemy 2") ||
				coll.gameObject.name.Contains ("Enemy Mito"))
				Death ();
		}
		else if (coll.gameObject.tag == "Player")
		{
			// hurt player
			self.decreasePlayerHealth(1);
		}
		else if (coll.gameObject.tag == "Bullet" 
			&& coll.gameObject.GetComponent<EnemyProjectile>().parent != this.gameObject)
		{
			Death ();
		}
	}

	private void Death ()
	{
		death.Death (ren, deathSpinMin, deathSpinMax);
	}
}
