using UnityEngine;
using System.Collections;

public class EnemyDeath : MonoBehaviour 
{
	public MonoBehaviour die;

	private bool dying = false;

	public void Death (SpriteRenderer ren, float deathSpinMin, float deathSpinMax)
	{
		if (dying) { return; }

		dying = true;

		// Find all of the sprite renderers on this object and it's children.
		SpriteRenderer[] otherRenderers = die.GetComponentsInChildren<SpriteRenderer> ();

		// Disable all of them sprite renderers.
		foreach (SpriteRenderer s in otherRenderers) { s.enabled = false; }

		// Re-enable the main sprite renderer and turn it's sprite red.
		ren.enabled = true;
		ren.color = Color.red;


		// Allow the enemy to rotate and spin it by adding a torque.
		die.GetComponent<Rigidbody2D>().fixedAngle = false;
		die.GetComponent<Rigidbody2D>().AddTorque (Random.Range (deathSpinMin, deathSpinMax));

		
		// Find all of the colliders on the gameobject and set them all to be triggers.
		Collider2D[] cols = die.GetComponents<Collider2D> ();
		foreach (Collider2D c in cols) { c.isTrigger = true; }

		Invoke ("Remove", 2.0f);
	}

	private void Remove () { Destroy (die.gameObject); }
}
