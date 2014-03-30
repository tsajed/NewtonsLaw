using UnityEngine;
using System.Collections;

public class TractorBeam : MonoBehaviour 
{
	public float power;
	public bool stasisEffect;
	public AudioClip pull;
	public AudioClip push;

	void FixedUpdate () 
	{
		if (Input.GetButton ("Fire2") || Input.GetButton ("Fire1"))
		{
			Vector3 clickLoc = Camera.main.ScreenToWorldPoint (Input.mousePosition);
			var myLoc = new Vector2 (transform.position.x, transform.position.y);
			//raycast needs a vector for the direction from the player.
			var target_direction = new Vector2 (clickLoc.x - myLoc.x, clickLoc.y - myLoc.y);
			target_direction.Normalize();
			target_direction.Scale (new Vector2 (50, 50));

			//if (Input.GetButton ("Fire2"))
			//	Debug.DrawRay (myLoc, target_direction, Color.blue, 1);
			//if (Input.GetButton ("Fire1"))
			//	Debug.DrawRay (myLoc, target_direction, Color.green, 1);

			//Set the line renderer.
			renderLine(myLoc, target_direction);

			//send a raycast and return all intersections. magn. gives distance to cast e.g. distance clicked from player
			RaycastHit2D[] all_hit = Physics2D.RaycastAll (myLoc, target_direction, target_direction.magnitude);
			foreach (RaycastHit2D hit in all_hit)
			{
				if (hit.collider == null)
					continue;

				if (hit.collider.gameObject.name != "Prototypi" && hit.collider.rigidbody2D != null
					&& !hit.collider.gameObject.name.Contains ("Projectile"))
				{


					if (stasisEffect)
						hit.collider.rigidbody2D.velocity = Vector2.zero; //reset velocity for stasis effect
					Vector2 clickLoc2d = new Vector2 (clickLoc.x, clickLoc.y);



					if (Input.GetButton ("Fire2"))
					{
						hit.collider.rigidbody2D.AddForce ((myLoc - clickLoc2d).normalized * power);
					}
					else
					{
						// Play push sound when found a hit when pushing
						if (Input.GetButton ("Fire1"))
							AudioSource.PlayClipAtPoint (push, hit.collider.transform.position);
						hit.collider.rigidbody2D.AddForce ((clickLoc2d - myLoc).normalized * power);
						break; // Only affect the first object hit.
					}
				}
			}
		}

		// Play the push sound when pushing
	/*	if(Input.GetButton("Fire1")) {
			//audio.clip = push;
			if(!audio.isPlaying)
				audio.Play();
		} */
		// Play the pull sound when pulling
		if (Input.GetButton ("Fire2"))
		{
			audio.clip = pull;
			if (!audio.isPlaying)
				audio.Play ();
		}
		else
		{
			audio.Stop ();
		}
	}
	void renderLine(Vector2 start, Vector2 end)
	{
		LineRenderer line = GetComponentInChildren<LineRenderer> ();
		line.SetPosition (0, start);
		line.SetPosition (1, start + end);

	}

}

