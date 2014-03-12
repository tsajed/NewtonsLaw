using UnityEngine;
using System.Collections;

public class EnemyShoot : MonoBehaviour 
{
	public Transform shotPrefab;
	public float shootingRate = 1.0f;
	public AudioClip laser;

	private float shootCooldown = 0f;

	public void TryShoot (Transform target)
	{
		if (shootCooldown > 0)
			shootCooldown -= Time.deltaTime;
		else
			Shoot (target);
	}

	private void Shoot (Transform target)
	{
		if (!CanAttack)
			return;

		shootCooldown = shootingRate;

		// Create a new shot
		var shotTransform = Instantiate (shotPrefab) as Transform;

		Vector3 diff = target.position - transform.position;
		// Assign position
		shotTransform.position = transform.position + (diff.normalized * 2);

		EnemyProjectile projectile = shotTransform.gameObject.GetComponent<EnemyProjectile> ();
		if (projectile != null)
		{


			projectile.oldTarget = new Vector3 (target.position.x, target.position.y);
			projectile.parent = this.gameObject;
		}

		audio.PlayOneShot (laser);
	}

	public bool CanAttack { get { return shootCooldown <= 0f; } }
}
