using UnityEngine;
using System.Collections;

public class Movement : MonoBehaviour {

	public float moveForce = 365f;			// Amount of force added to move the player left and right.
	public float maxSpeed = 5f;				// The fastest the player can travel in the x axis.
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void FixedUpdate () {
		// Cache the horizontal input.
		float h = Input.GetAxis("Horizontal");
		float v = Input.GetAxis("Vertical");

		// If the player is changing direction (h has a different sign to velocity.x) or hasn't reached maxSpeed yet...
		if(h * rigidbody2D.velocity.x < maxSpeed)
			// ... add a force to the player.
			rigidbody2D.AddForce(Vector2.right * h * moveForce);

		// If the player's horizontal velocity is greater than the maxSpeed...
		if(Mathf.Abs(rigidbody2D.velocity.x) > maxSpeed)
			// ... set the player's velocity to the maxSpeed in the x axis.
			rigidbody2D.velocity = new Vector2(Mathf.Sign(rigidbody2D.velocity.x) * maxSpeed, rigidbody2D.velocity.y);


		if(v * rigidbody2D.velocity.x < maxSpeed)
			rigidbody2D.AddForce(Vector2.up * v * moveForce);

		if(Mathf.Abs(rigidbody2D.velocity.y) > maxSpeed)
			rigidbody2D.velocity = new Vector2(rigidbody2D.velocity.x, Mathf.Sign(rigidbody2D.velocity.y) * maxSpeed);

		//push/pull
		if ((Input.GetButton("Jump") || Input.GetButton("Fire2")) || Input.GetButton("Fire1")) {
			Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition); 
			Vector2 origin = new Vector2(ray.origin.x,ray.origin.y); 
			Vector2 myLoc = new Vector2(transform.position.x,transform.position.y);
			if(Input.GetButton("Jump") || Input.GetButton("Fire2"))
				Debug.DrawLine(origin,myLoc, Color.blue,1);
			if(Input.GetButton ("Fire1"))
				Debug.DrawLine(origin,myLoc, Color.green,1);
			RaycastHit2D hit = Physics2D.Linecast(origin,myLoc);
			print ("origin: " + origin);
			print ("myLoc: " + myLoc);
			if(hit.collider != null){
				if (hit.collider.gameObject.name != "Prototypi" && hit.collider.rigidbody2D != null){
					int dir = 1;
					if (Input.GetButton("Fire1")) dir = -1;
					hit.collider.rigidbody2D.AddForce( (myLoc - origin) * dir);
				}
			}
		}

	}
}
