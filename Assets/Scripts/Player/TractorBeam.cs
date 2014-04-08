using UnityEngine;
using System.Collections;

public class TractorBeam : MonoBehaviour 
{
	public float power;
	public bool stasisEffect;
	public AudioClip pull;
	public AudioClip push;
	public Material[] lineMaterials;

	private LineRenderer line;
	private ParticleSystem particles;
	void Start () 
	{
		line = GetComponentInChildren<LineRenderer> ();
		SpriteRenderer sprite = GetComponentInChildren<SpriteRenderer> ();
		line.renderer.sortingLayerID = sprite.renderer.sortingLayerID;
		line.renderer.sortingOrder = sprite.renderer.sortingOrder;

		particles = GetComponentInChildren<ParticleSystem> ();
	}

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
			int materialIndex;
			if (Input.GetButton ("Fire2"))
				materialIndex = 0;
			else
				materialIndex = 1;

			//Set the line renderer.
			line.gameObject.SetActive(true);
			renderLine(myLoc, target_direction, materialIndex);

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

	void Update() 
	{
		// Turn off the line renderer once the player lets go of the button
		if(Input.GetButtonUp("Fire2") || Input.GetButtonUp("Fire1") )
		{
			line.gameObject.SetActive(false);
		}
	}
	
	void renderLine(Vector2 start, Vector2 end, int materialIndex)
	{
		line.material = lineMaterials[materialIndex];
		line.SetPosition (0, start);
		line.SetPosition (1, start + end);

		if (materialIndex == 0) 
		{
			particles.startColor = Color.cyan;
		} 
		else 
		{
			particles.startColor = Color.green;
		}
		particles.transform.rotation = Quaternion.LookRotation (end);
		particles.Play ();
	}

}

