using UnityEngine;
using System.Collections;


public class MitoEnemy : MonoBehaviour 
{
	public float speed = 10;
	public int health = 100;
	public float damage = 2.0f;

	public GenericEnemy self;
	public Transform target;

	// A value to give the minimum amount of Torque when dying
	public float deathSpinMin = -100f;
	// A value to give the maximum amount of Torque when dying
	public float deathSpinMax = 50f;

	/// <summary>
	/// Projectile prefab for shooting
	/// </summary>
	public GameObject shotPrefab;

	/// <summary>
	/// Cooldown in seconds between two shots
	/// </summary>
	public float shootingRate = 12;
	public int spawns = 3;

	public MitoEnemy parent;

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

		shootCooldown = shootingRate; // Prevent spawning immediately
	}

	void Update ()
	{
		if (dying) { return; }

		if (shootCooldown > 0) { shootCooldown -= Time.deltaTime; }
		else { Spawn (false); }
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

	private void Spawn (bool isEnemy)
	{
		if (!CanDivide) { return; }

		shootCooldown = shootingRate;
		spawns--;

		// Create a new shot
		var shotPrefabInst = Instantiate (shotPrefab) as GameObject;

		// Assign position
		shotPrefabInst.transform.position = transform.position;

		var spawn = shotPrefabInst.GetComponent<MitoEnemy> ();
		if (spawn != null)
		{
			var spawnBody = spawn.transform.Find ("body").GetComponent<Transform> ();
			spawnBody.localScale = new Vector3 (spawnBody.localScale.x * 0.65f, spawnBody.localScale.y * 0.65f);
			spawn.parent = this;
			spawn.spawns = this.spawns;
			spawn.speed *= 1.2f;
		}

		/* shit's exponential yo
		 
			start3 -> spawnb2
			start2 -> spawnc1
			start1 -> spawnd0

			spawnb2 -> spawnba1
			spawnb1 -> spawnbb0

			spawnba1 -> spawnbaa0

			spawnc1 -> spawnca0
		 */

		//audio.PlayOneShot (<<audio>>);
	}

	private void Death ()
	{
		if (this.parent) { this.parent.spawns++; }
		dying = true;
		score.createScore ();
		death.Death (ren, deathSpinMin, deathSpinMax);
	}

	public bool CanDivide 
	{
		get { return shootCooldown <= 0f && spawns > 0; }
	}
}
