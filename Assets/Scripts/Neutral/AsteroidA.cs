using UnityEngine;
using System.Collections;


public class AsteroidA : MonoBehaviour 
{
	public float speed = 50;
	public int health = 100;
	public float damage = 2.0f;
	public GenericEnemy self;
	public Transform target;

	private EnemyMovement move;

	void Start () // Use this for initialization
	{
		self = new GenericEnemy(this.gameObject, health, speed, damage);

		var throwaway = new GameObject();
		throwaway.transform.position = transform.position;
		if (!target) { target = throwaway.transform; }

		move = this.GetComponent<EnemyMovement> ();
		move.location = this.transform;
		move.self = this.self;
	}

	void FixedUpdate () // Update is called once per frame
	{
		RandomTarget ();

		move.TryMove (target); 
	}

	void OnCollisionEnter2D(Collision2D coll)
	{
		// right now doesn't affect anything
	}

	private void RandomTarget ()
	{
		if (Vector2.Distance (target.transform.position, this.transform.position) < 5)
		{
			var distanceModifier = 50;
			// random target
			Vector2 i = Random.insideUnitCircle * distanceModifier;
			target.position = new Vector3 (i.x + this.transform.position.x,
				i.y + this.transform.position.y);
		}
	}
}
