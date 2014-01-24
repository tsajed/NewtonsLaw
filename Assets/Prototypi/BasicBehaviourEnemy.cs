using UnityEngine;
using System.Collections;

public class BasicBehaviourEnemy : MonoBehaviour {

	public GenericEnemy self;
	public Transform target;

	private SpriteRenderer ren;			// Reference to the sprite renderer.
	public float deathSpinMin = -100f;			// A value to give the minimum amount of Torque when dying
	public float deathSpinMax = 100f;			// A value to give the maximum amount of Torque when dying

	// Use this for initialization
	void Start () {
		self = new GenericEnemy(this.gameObject, 100, 2.0f, 2.0f);
		// Setting up the references.
		ren = transform.Find("body").GetComponent<SpriteRenderer>();

		if(!target)
			target = GameObject.FindWithTag("Player").transform;
	}
	
	// Update is called once per frame
	void FixedUpdate () {

		// Set the enemy's velocity to moveSpeed in the x direction.
		if(target.position.x < transform.position.x - 1.2)
			rigidbody2D.velocity = new Vector2(-transform.localScale.x * self.getSpeed(), rigidbody2D.velocity.y);	
		else if(target.position.x > transform.position.x + 1.2)
			rigidbody2D.velocity = new Vector2(transform.localScale.x * self.getSpeed(), rigidbody2D.velocity.y);
		else if(target.position.x == transform.position.x)
			rigidbody2D.velocity = new Vector2(0, rigidbody2D.velocity.y);

		// Set the enemy's velocity to moveSpeed in the y direction.
		if(target.position.y < transform.position.y - 1.2)
			rigidbody2D.velocity = new Vector2(rigidbody2D.velocity.x, -transform.localScale.y * self.getSpeed());
		else if(target.position.y > transform.position.y + 1.2)
			rigidbody2D.velocity = new Vector2(rigidbody2D.velocity.x, transform.localScale.y * self.getSpeed());
		else if(target.position.y == transform.position.y)
			rigidbody2D.velocity = new Vector2(rigidbody2D.velocity.x, 0);

		if(self.getHealth() <= 0) {
			Destroy(this.gameObject);
		}
	}

	void OnCollisionEnter2D(Collision2D coll) {
	    if (coll.gameObject.tag == "Enemy")
	    	Death();
	    if(coll.gameObject.tag == "Player") {
	   		Death();
	   	}
	}

	void Death()
	{
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

	private void Remove() {
		Destroy(gameObject);
	}
}
