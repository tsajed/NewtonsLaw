using UnityEngine;
using System.Collections;


public class EnemyHeatSeekingProjectile : MonoBehaviour 
{
	public GenericEnemy self;
	public Transform target { private get; set; }
	public GameObject parent;

	void Start ()
	{
		self = new GenericEnemy (this.gameObject, 100, 15f, 2.0f);
		// 2 - Limited time to live to avoid any leak
		Destroy (gameObject, 15); // seconds 
	}

	void FixedUpdate ()
	{
		Vector3 dir = target.position - this.transform.position;
		Vector2 force = dir.normalized * self.movementSpeed;
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
			if (coll.gameObject.name.Contains ("Force Field"))
			{
				if (coll.gameObject.GetComponent<ForceFieldEnemy> ().shieldsDown)
				{ Destroy (gameObject); }
			}
			else
			{
				Destroy (gameObject);
			}
		}
	}
}
