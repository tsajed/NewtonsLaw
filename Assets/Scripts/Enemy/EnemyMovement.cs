using UnityEngine;
using System.Collections;

public class EnemyMovement : MonoBehaviour 
{
	public Transform location;
	public GenericEnemy self;

	public float stunCooldown = 0f;

	private Rigidbody2D rigidBody2D;

	void Awake() {
		rigidBody2D = GetComponent<Rigidbody2D>();
	}

	public void TryMove (Transform target)
	{
		if (stunCooldown > 0) { stunCooldown -= Time.deltaTime; }
		else { Move (target); }
	}

	private void Move (Transform target)
	{
		Vector3 dir = target.transform.position - location.position;
		Vector2 force = dir.normalized * self.movementSpeed;
		rigidBody2D.AddForce (force);
	}
}
