using UnityEngine;
using System.Collections;

public class EnemyMovement : MonoBehaviour 
{
	public Transform location;
	public GenericEnemy self;

	public float stunCooldown = 0f;

	public void TryMove (Transform target)
	{
		if (stunCooldown > 0)
			stunCooldown -= Time.deltaTime;
		else
			Move (target);
	}

	private void Move (Transform target)
	{
		Vector3 dir = target.transform.position - location.position;
		dir = dir.normalized;
		Vector2 force = dir * self.movementSpeed;
		rigidbody2D.AddForce (force);
	}
}
