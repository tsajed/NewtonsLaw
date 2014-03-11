using UnityEngine;
using System.Collections;

public class EnemyMovement : MonoBehaviour 
{
	public void Move (MonoBehaviour move, Transform target, GenericEnemy self)
	{
		Vector3 dir = target.transform.position - move.transform.position;
		dir = dir.normalized;
		Vector2 force = dir * self.movementSpeed;
		rigidbody2D.AddForce (force);
	}
}
