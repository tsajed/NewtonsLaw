using UnityEngine;
using System.Collections;

public class EnemyDeath : MonoBehaviour 
{
	private bool dying;

	// Use this for initialization
	void Start () 
	{
		dying = false;
	}

	public void Death (MonoBehaviour die, SpriteRenderer ren, float deathSpinMin, float deathSpinMax)
	{
		if (dying)
			return;

		dying = true;

		// Find all of the sprite renderers on this object and it's children.
		SpriteRenderer[] otherRenderers = GetComponentsInChildren<SpriteRenderer> ();

		// Disable all of them sprite renderers.
		foreach (SpriteRenderer s in otherRenderers)
			s.enabled = false;

		// Re-enable the main sprite renderer and set it's sprite to the deadEnemy sprite.
		ren.enabled = true;
		//ren.sprite = deadEnemy;

		// Set dead to true.
		//dead = true;

		// Allow the enemy to rotate and spin it by adding a torque.
		rigidbody2D.fixedAngle = false;
		rigidbody2D.AddTorque (Random.Range (deathSpinMin, deathSpinMax));

		// Find all of the colliders on the gameobject and set them all to be triggers.
		Collider2D[] cols = GetComponents<Collider2D> ();
		foreach (Collider2D c in cols)
			c.isTrigger = true;

		Invoke ("Remove", 2.0f);
	}

	private void Remove () { Destroy (gameObject); }
}
