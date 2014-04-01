using UnityEngine;
using System.Collections;


public class ExplodingEnemy : MonoBehaviour 
{
	public float speed = 5;
	public int health = 100;
	public float damage = 2.0f;
	public GenericEnemy self;
	public Transform target;

	public float deathSpinMin = -100f; // A value to give the minimum amount of Torque when dying
	public float deathSpinMax = 100f;	// A value to give the maximum amount of Torque when dying

	/// <summary>
	/// Projectile prefab for shooting
	/// </summary>
	public GameObject[] shotPrefab;

	/// <summary>
	/// Cooldown in seconds between two shots
	/// </summary>
	public float shootingRate = 3;

	//
	// 2  Cooldown
	//
	private float shootCooldown = 0f;

	public Sprite flash;

	private EnemyDeath death;
	private EnemyMovement move;
	private EnemyShoot shoot;
	private EnemyScore score;
	private SpriteRenderer ren;	// Reference to the sprite renderer.
	private bool dying = false;

	void Start () // Use this for initialization
	{
		self = new GenericEnemy(this.gameObject, health, speed, damage);
		// Setting up the references.
		ren = transform.Find("body").GetComponent<SpriteRenderer>();

		var throwaway = new GameObject();
		throwaway.transform.position = transform.position;
		if (!target) { target = throwaway.transform; }

		death = this.GetComponent<EnemyDeath> ();
		death.die = this;
		move = this.GetComponent<EnemyMovement> ();
		move.location = this.transform;
		move.self = this.self;
		shoot = this.GetComponent<EnemyShoot> ();
		//shoot.sound = this.laser;
		shoot.shotPrefab = this.shotPrefab;
		shoot.shootingRate = this.shootingRate;
		score = this.GetComponent<EnemyScore> ();
		score.self = this.self;
	}

	void Update ()
	{
		if (shootCooldown > 0) 
		{ 
			shootCooldown -= Time.deltaTime; 
		}
		else 
		{ 
			shootCooldown = shootingRate;
			Sprite temp = ren.sprite;
			ren.sprite = flash;
			flash = temp;
		}
	}

	void FixedUpdate () // Update is called once per frame
	{
		if (dying) { return; }

		if (self.health <= 0) { Death (); }

		RandomTarget ();

		move.TryMove (target); 
	}

	void OnCollisionEnter2D(Collision2D coll)
	{
		if (dying) { return; }

		if (coll.gameObject.tag == "Enemy")
		{
			Death (); // explode
		}
		else if (coll.gameObject.tag == "Player")
		{
			// hurt self instead of player
			Death (); // explode
		}
		else if (coll.gameObject.tag == "Bullet")
		{
			Death (); // explode
		}
	}

	private void RandomTarget ()
	{
		if (Vector2.Distance (target.transform.position, this.transform.position) < 5)
		{
			// random target
			Vector2 i = Random.insideUnitCircle * 25;
			target.position = new Vector3 (i.x + this.transform.position.x,
				i.y + this.transform.position.y);
		}
	}

	private void Death ()
	{
		if (!shoot.child) { shoot.TryShoot (target, 0); } // explode
		dying = true;
		score.createScore ();
		death.Death (ren, deathSpinMin, deathSpinMax);
	}
}
