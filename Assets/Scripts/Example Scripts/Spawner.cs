using UnityEngine;
using System.Collections;

public class Spawner : MonoBehaviour
{
	public float spawnTime = 5f;		// The amount of time between each spawn.
	public float spawnDelay = 3f;		// The amount of time before spawning starts.
	public float effectTime = 3f;		// The amount of time the spawn warning effect plays for.
	public GameObject[] enemies;		// Array of enemy prefabs.
	ParticleSystem particles;


	void Start ()
	{
		// Start calling the Spawn function repeatedly after a delay .
		InvokeRepeating("Spawn", spawnDelay, spawnTime);
		particles = GetComponentInChildren<ParticleSystem> ();
	}


	void Spawn ()
	{
		Invoke ("Create", effectTime);

		particles.Play ();
	}
	void Create ()
	{
		// Instantiate a random enemy.
		int enemyIndex = Random.Range(0, enemies.Length);
		Instantiate(enemies[enemyIndex], transform.position, transform.rotation);
		particles.Stop ();

	}
}
