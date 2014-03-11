using UnityEngine;
using System.Collections;

public class MitoEnemy : MonoBehaviour 
{
	public float speed = 15;
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
	public Transform shotPrefab;

	/// <summary>
	/// Cooldown in seconds between two shots
	/// </summary>
	public float shootingRate = 5;
	public int spawns = 3;

	public Vector3 oldTarget { private get; set; }
	public MitoEnemy parent;

	//
	// 2  Cooldown
	//
	private float shootCooldown = 0f;
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

	void Update ()
	{
		if (dying)
			return;

		if (shootCooldown > 0)
			shootCooldown -= Time.deltaTime;
		else
			Spawn (false);
	}

	void FixedUpdate ()
	{
		if (dying)
			return;

		if (self.health <= 0)
			Death ();

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
			Death ();
		}
	}

	private void Spawn (bool isEnemy)
	{
		if (!CanDivide)
			return;

		shootCooldown = shootingRate;
		spawns--;

		// Create a new shot
		var shotTransform = Instantiate (shotPrefab) as Transform;

		// Assign position
		shotTransform.position = transform.position;

		MitoEnemy spawn = shotTransform.gameObject.GetComponent<MitoEnemy> ();
		if (spawn != null)
		{
			var spawnBody = spawn.transform.Find ("body").GetComponent<Transform> ();
			spawnBody.localScale = new Vector3 (spawnBody.localScale.x * 0.65f, spawnBody.localScale.y * 0.65f);
			spawn.oldTarget = new Vector3 (target.position.x, target.position.y);
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
		if (this.parent)
			this.parent.spawns--;
		death.Death (this, ren, deathSpinMin, deathSpinMax);
	}

	public bool CanDivide 
	{ 
		get 
		{ 
			return shootCooldown <= 0f && spawns > 0; 
		} 
	}
}
