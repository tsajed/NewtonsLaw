using UnityEngine;
using System.Collections;

public class EnemyProjectile : MonoBehaviour 
{
	public GenericEnemy self;
	public Vector3 oldTarget { private get;  set; }
	public GameObject parent;

	private bool stop = false;

	// Use this for initialization
	void Start ()
	{
		float p_velocity = parent.rigidbody2D.velocity.magnitude;
		self = new GenericEnemy (this.gameObject, 100, 50f, 2.0f);
		// 2 - Limited time to live to avoid any leak
		Destroy (gameObject, 12); // seconds

		//Apply all force in beginning
		var dir = oldTarget - this.transform.position;
		var force = dir * (self.movementSpeed + p_velocity);
		rigidbody2D.AddForce (force);
	}

	// Update is called once per frame
	void FixedUpdate ()
	{
		if (stop)
			return;


	}

	void OnTriggerEnter2D (Collider2D coll)
	{
		print ("beep");
		if (coll.gameObject.tag == "Player")
		{
			// hurt player
			self.decreasePlayerHealth(1);
			Destroy (gameObject);
		}
		else if (coll.gameObject.tag == "Enemy" && coll.gameObject.name != "Enemy 1")
		{
			Destroy (gameObject);
		}
	}
}
