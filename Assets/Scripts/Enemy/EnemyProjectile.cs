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
		self = new GenericEnemy (this.gameObject, 100, 0.01f, 2.0f);
		// 2 - Limited time to live to avoid any leak
		Destroy (gameObject, 4); // seconds
	}

	// Update is called once per frame
	void FixedUpdate ()
	{
		if (stop)
			return;

		Vector3 dir = oldTarget - this.transform.position;
		dir = dir.normalized;
		Vector2 force = dir * self.movementSpeed;
		rigidbody2D.AddForce (force);
	}

	void OnCollisionEnter2D (Collision2D coll)
	{
		if (coll.gameObject.tag == "Player")
		{
			// hurt player
			self.decreasePlayerHealth(1);
			Destroy (gameObject);
		}
		else if (coll.gameObject.tag == "Enemy" && coll.gameObject != parent)
		{
			Destroy (gameObject);
		}
	}
}
