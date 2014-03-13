using UnityEngine;
using System.Collections;


public class EnemyShoot : MonoBehaviour 
{
	public Transform shotPrefab;
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
		var shotTransform = Instantiate (shotPrefab) as Transform;
		child = shotTransform;

		// Assign position
		shotTransform.position = transform.position;

		EnemyProjectile projectile = shotTransform.gameObject.GetComponent<EnemyProjectile> ();
		if (projectile != null)
		{
			projectile.oldTarget = new Vector3 (target.position.x, target.position.y);
			projectile.parent = this.gameObject;
		}
		EnemyLaser laser = shotTransform.gameObject.GetComponent<EnemyLaser> ();
		if (laser != null)
		{
			laser.parent = this.gameObject;
		}

		audio.PlayOneShot (sound);
	}
}
