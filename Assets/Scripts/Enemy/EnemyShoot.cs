using UnityEngine;
using System.Collections;


public class EnemyShoot : MonoBehaviour 
{
	public GameObject[] shotPrefab;
	public float shootingRate = 1.0f;
	public AudioClip sound;
	public Transform child;

	private float shootCooldown = 0f;

	public void TryShoot (Transform target, int index)
	{
		if (shootCooldown > 0)
		{
			shootCooldown -= Time.deltaTime;
		}
		else
		{
			shootCooldown = shootingRate;
			Shoot (target, index);
		}
	}

	private void Shoot (Transform target, int index)
	{
		// Create a new shot
		var shotPrefabInst = Instantiate (shotPrefab[index]) as GameObject;
		child = shotPrefabInst.transform;

		Vector3 diff = target.position - transform.position;
		// Assign position
		shotPrefabInst.transform.position = transform.position + (diff.normalized * 2);

		SetComponentParameters (shotPrefabInst, target);

		audio.PlayOneShot (sound);
	}

	private void SetComponentParameters(GameObject shotPrefabInst, Transform target)
	{
		var projectile = shotPrefabInst.GetComponent<EnemyProjectile> ();
		if (projectile != null)
		{
			projectile.oldTarget = new Vector3 (target.position.x, target.position.y);
			projectile.parent = this.gameObject;
		}

		var heatSeekingProjectile = shotPrefabInst.GetComponent<EnemyHeatSeekingProjectile> ();
		if (heatSeekingProjectile != null)
		{
			heatSeekingProjectile.target = target;
			heatSeekingProjectile.parent = this.gameObject;
		}

		var laser = shotPrefabInst.GetComponent<EnemyLaser> ();
		if (laser != null)
		{ laser.parent = this.gameObject; }

		var explosion = shotPrefabInst.GetComponent<EnemyExplosion> ();
		if (explosion != null)
		{ explosion.parent = this.gameObject; }
	}
}
