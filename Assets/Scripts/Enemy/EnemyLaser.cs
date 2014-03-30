using UnityEngine;
using System.Collections;


public class EnemyLaser : MonoBehaviour 
{
	public GenericEnemy self;
	public GameObject parent;

	public float impact = 750;

	// Use this for initialization
	void Start ()
	{
		self = new GenericEnemy (this.gameObject, 100, 50f, 2.0f);
		// 2 - Limited time to live to avoid any leak
		Destroy (gameObject, 12); // seconds
	}

	void Update ()
	{
		if (this.parent) { this.transform.position = this.parent.transform.position; }
		else { Destroy (gameObject); }
	}

	void OnTriggerEnter2D (Collider2D coll)
	{
		if (coll.gameObject.tag == "Player")
		{
			// player can't move
			coll.gameObject.GetComponent<PlayerMovement> ().stunCooldown = 0.5f;
			
			// hurt player
			// put timer on this so it doesn't kill them
			self.decreasePlayerHealth(1);

			// push back player don't destroy
			//Destroy (gameObject);

			PushAway (coll);
		}
		else if (coll.gameObject.tag == "Enemy" && coll.gameObject != parent)
		{
			// stun
			coll.gameObject.GetComponent<EnemyMovement> ().stunCooldown = 0.5f;

			PushAway (coll);
		}
	}

	private void PushAway (Collider2D coll)
	{
		coll.transform.LookAt (this.transform.position);
		coll.transform.eulerAngles = new Vector3 (0, transform.eulerAngles.y, 0);

		Vector3 dir = coll.transform.position - this.transform.position; // away
		//Vector3 dir = this.transform.position - coll.transform.position; // towards
		dir = dir.normalized;
		Vector2 force = dir * impact;
		coll.rigidbody2D.AddForce (force);
	}
}
