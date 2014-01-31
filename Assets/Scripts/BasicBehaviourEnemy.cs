using UnityEngine;
using System.Collections;

public class BasicBehaviourEnemy : MonoBehaviour 
{
	public GenericEnemy self;
	public Transform target;

	private SpriteRenderer ren;			// Reference to the sprite renderer.
	public float deathSpinMin = -100f;			// A value to give the minimum amount of Torque when dying
	public float deathSpinMax = 100f;			// A value to give the maximum amount of Torque when dying

	private bool dying;

	// Use this for initialization
	void Start () 
	{
		self = new GenericEnemy(this.gameObject, 100, 0.001f, 2.0f);
		// Setting up the references.
		ren = transform.Find("body").GetComponent<SpriteRenderer>();

		if(!target)
			target = GameObject.FindWithTag("Player").transform;

		dying = false;
	}
	
	// Update is called once per frame
	void FixedUpdate () 
	{
		if (self.health <= 0)
			Death ();

		if (dying)
			return;

		var dir = target.transform.position - this.transform.position;
		dir = dir.normalized;
		var force = dir * self.movementSpeed;
		rigidbody2D.AddForce (force);
	}

	void OnCollisionEnter2D(Collision2D coll)
	{
		if (coll.gameObject.tag == "Enemy")
		{
			if (coll.gameObject.name == "Enemy 2")
				Death ();
		}
		else if (coll.gameObject.tag == "Player")
		{
			// hurt self instead of player
			Death ();
		}
		else if (coll.gameObject.tag == "Bullet"
			&& coll.gameObject.GetComponent<Projectile> ().parent != this.gameObject)
		{
			Death ();
		}
	}

	public void Death()
	{
		dying = true;

		// Find all of the sprite renderers on this object and it's children.
		SpriteRenderer[] otherRenderers = GetComponentsInChildren<SpriteRenderer>();

		// Disable all of them sprite renderers.
		foreach(SpriteRenderer s in otherRenderers)
		{
			s.enabled = false;
		}

		// Re-enable the main sprite renderer and set it's sprite to the deadEnemy sprite.
		ren.enabled = true;
		//ren.sprite = deadEnemy;

		// Set dead to true.
		//dead = true;

		// Allow the enemy to rotate and spin it by adding a torque.
		rigidbody2D.fixedAngle = false;
		rigidbody2D.AddTorque(Random.Range(deathSpinMin,deathSpinMax));

		// Find all of the colliders on the gameobject and set them all to be triggers.
		Collider2D[] cols = GetComponents<Collider2D>();
		foreach(Collider2D c in cols)
		{
			c.isTrigger = true;
		}

		Invoke("Remove", 2.0f);
	}

	private void Remove() {	Destroy(gameObject); }
}
