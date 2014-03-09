using UnityEngine;
using System.Collections;

public class BasicBehaviourEnemy : MonoBehaviour 
{
	public float speed;
	public GenericEnemy self;
	public Transform target;

	private SpriteRenderer ren;			// Reference to the sprite renderer.
	public float deathSpinMin = -100f;			// A value to give the minimum amount of Torque when dying
	public float deathSpinMax = 100f;			// A value to give the maximum amount of Torque when dying
	private EnemyDeath death;	

	// Use this for initialization
	void Start () 
	{
		self = new GenericEnemy(this.gameObject, 100, speed, 2.0f);
		// Setting up the references.
		ren = transform.Find("body").GetComponent<SpriteRenderer>();

		if(!target)
			target = GameObject.FindWithTag("Player").transform;

		death = this.GetComponent<EnemyDeath> ();
	}
	
	// Update is called once per frame
	void FixedUpdate () 
	{
		if (self.health <= 0)
			death.Death (this, ren, deathSpinMin, deathSpinMax);

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
				death.Death (this, ren, deathSpinMin, deathSpinMax);
		}
		else if (coll.gameObject.tag == "Player")
		{
			// hurt self instead of player
			//Death ();
		}
		else if (coll.gameObject.tag == "Bullet"
			&& coll.gameObject.GetComponent<EnemyProjectile> ().parent != this.gameObject)
		{
			death.Death (this, ren, deathSpinMin, deathSpinMax);
		}
	}
}
