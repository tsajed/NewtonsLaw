using UnityEngine;
using System.Collections;


public class EnemyProjectile : MonoBehaviour 
{
	public GenericEnemy self;
	public Vector3 oldTarget { private get; set; }
	public GameObject parent;
	public float maxMagnitude;

	private Rigidbody2D rigidBody2D;


	void Start ()
	{
		rigidBody2D = parent.GetComponent<Rigidbody2D>();
		float p_velocity = rigidBody2D.velocity.magnitude;
		self = new GenericEnemy (this.gameObject, 100, 50f, 2.0f);
		// 2 - Limited time to live to avoid any leak
		Destroy (gameObject, 12); // seconds 

		//Apply all force in beginning
		Vector3 dir = oldTarget - this.transform.position;
		Vector2 force = dir * (self.movementSpeed + p_velocity);
		Vector2 clamped = Vector2.ClampMagnitude (force, maxMagnitude);
		rigidBody2D.AddForce (clamped);
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
