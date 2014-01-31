﻿using UnityEngine;

public class ShootingEnemy : MonoBehaviour
{
	public GenericEnemy self;
	public Transform target;

	private SpriteRenderer ren;
	// A value to give the minimum amount of Torque when dying
	public float deathSpinMin = 100f;
	// A value to give the maximum amount of Torque when dying
	public float deathSpinMax = 100f; 

	/// <summary>
	/// Projectile prefab for shooting
	/// </summary>
	public Transform shotPrefab;

	/// <summary>
	/// Cooldown in seconds between two shots
	/// </summary>
	public float shootingRate;

	//
	// 2  Cooldown
	//
	private float shootCooldown;

	private bool dying;

	void Start () // initialization
	{
		self = new GenericEnemy (this.gameObject, 100, 0.002f, 2.0f);
		ren = transform.Find ("body").GetComponent<SpriteRenderer> ();

		if (!target)
			target = GameObject.FindWithTag ("Player").transform;

		shootCooldown = 0f;
		dying = false;
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
		if (self.health <= 0)
			Death ();

		if (dying)
			return;

		if (Vector2.Distance (target.transform.position, this.transform.position) < 15)
			return;

		var dir = target.transform.position - this.transform.position;
		dir = dir.normalized;
		var force = dir * self.movementSpeed;
		rigidbody2D.AddForce (force);
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

		Projectile projectile = shotTransform.gameObject.GetComponent<Projectile> ();
		if (projectile != null)
		{
			projectile.oldTarget = new Vector3 (target.position.x, target.position.y);
			projectile.parent = this.gameObject;
		}
	}

	public bool CanAttack { get { return shootCooldown <= 0f; } }

	private void OnCollisionEnter2D (Collision2D coll)
	{
		if (coll.gameObject.tag == "Enemy")
		{
			if (coll.gameObject.name == "Enemy 2")
				Death ();
		}
		else if (coll.gameObject.tag == "Player")
		{
			// hurt player
		}
		else if (coll.gameObject.tag == "Bullet" 
			&& coll.gameObject.GetComponent<Projectile>().parent != this.gameObject)
		{
			Death ();
		}
	}

	public void Death ()
	{
		dying = true;

		// Find all of the sprite renderers on this object and it's children.
		SpriteRenderer[] otherRenderers = GetComponentsInChildren<SpriteRenderer> ();

		// Disable all of them sprite renderers.
		foreach (SpriteRenderer s in otherRenderers)
			s.enabled = false;

		// Reenable the main sprite renderer and set it's sprite to the deadEnemy sprite.
		ren.enabled = true;

		// Allow the enemy to rotate and spin it by adding a torque.
		rigidbody2D.fixedAngle = false;
		rigidbody2D.AddTorque (Random.Range (deathSpinMin, deathSpinMax));

		// Find all of the colliders on the gameobject and set them all to be triggers.
		Collider2D[] cols = GetComponents<Collider2D> ();
		foreach (Collider2D c in cols)
			c.isTrigger = true;

		Invoke ("Remove", 2.0f);
	}

	private void Remove () { Destroy (gameObject); }
}
