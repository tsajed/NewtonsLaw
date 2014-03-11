using UnityEngine;
using System.Collections;

public class EnemyMovement : MonoBehaviour 
{
	public Transform transform;
	public GenericEnemy self;

	public void Move (Transform target)
	{
		Vector3 dir = target.transform.position - transform.position;
		dir = dir.normalized;
		Vector2 force = dir * self.movementSpeed;
		rigidbody2D.AddForce (force);
	}
}
