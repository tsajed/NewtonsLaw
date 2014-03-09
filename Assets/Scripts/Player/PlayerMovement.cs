using UnityEngine;
using System.Collections;

public class PlayerMovement : MonoBehaviour 
{
	public float moveForce = 365f;			// Amount of force added to move the player left and right.
	public float maxSpeed = 5f;				// The fastest the player can travel in the x axis.
	
	// Update is called once per frame
	void FixedUpdate () 
	{
		// Cache the horizontal input.
		float h = Input.GetAxis("Horizontal");
		float v = Input.GetAxis("Vertical");
		
		/*// If the player is changing direction (h has a different sign to velocity.x) or hasn't reached maxSpeed yet...
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
		*/
		
		// Set a maximum velocity, don't stop add force when you're over the max velocity!
		rigidbody2D.AddForce(new Vector2(h * moveForce, v * moveForce));
		if (rigidbody2D.velocity.x > maxSpeed) {
			rigidbody2D.velocity = new Vector2(maxSpeed, rigidbody2D.velocity.y);
		} else if (rigidbody2D.velocity.x < -maxSpeed) {
			rigidbody2D.velocity = new Vector2(-maxSpeed, rigidbody2D.velocity.y);
		}
		if (rigidbody2D.velocity.y > maxSpeed) {
			rigidbody2D.velocity = new Vector2(rigidbody2D.velocity.x, maxSpeed);
		} else if (rigidbody2D.velocity.y < -maxSpeed) {
			rigidbody2D.velocity = new Vector2(rigidbody2D.velocity.x, -maxSpeed);
		}
	}
}
