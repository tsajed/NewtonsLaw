using UnityEngine;
using System.Collections;


public class EnemyShoot : MonoBehaviour 
{
	public Transform[] shotPrefab;
	public float shootingRate = 1.0f;
	public AudioClip sound;
	public Transform child;

	private float shootCooldown = 0f;

	public void TryShoot (Transform target)
	{
		if (shootCooldown > 0)
		{
			shootCooldown -= Time.deltaTime;
		}
		else
		{
			shootCooldown = shootingRate;
			Shoot (target);
		}
	}

	private void Shoot (Transform target)
	{
		// Create a new shot
		var shotTransform = Instantiate (shotPrefab[0]) as Transform;
		child = shotTransform;

		Vector3 diff = target.position - transform.position;
		// Assign position
		shotTransform.position = transform.position + (diff.normalized * 2);

		SetComponentParameters (shotTransform, target);

		audio.PlayOneShot (sound);
	}

	private void SetComponentParameters(Transform shotTransform, Transform target)
	{
		var projectile = shotTransform.gameObject.GetComponent<EnemyProjectile> ();
		if (projectile != null)
		{
			projectile.oldTarget = new Vector3 (target.position.x, target.position.y);
			projectile.parent = this.gameObject;
		}
		var laser = shotTransform.gameObject.GetComponent<EnemyLaser> ();
		if (laser != null)
		{
			laser.parent = this.gameObject;
		}
		var explosion = shotTransform.gameObject.GetComponent<EnemyExplosion> ();
		if (explosion != null)
		{
			explosion.parent = this.gameObject;
		}
	}
}
