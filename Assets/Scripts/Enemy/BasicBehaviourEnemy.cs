using UnityEngine;
using System.Collections;

public class BasicBehaviourEnemy : MonoBehaviour 
{
	public float speed = 5;
	public int health = 100;
	public float damage = 2.0f;
	public GenericEnemy self;
	public Transform target;

	public float deathSpinMin = -100f; // A value to give the minimum amount of Torque when dying
	public float deathSpinMax = 100f;	// A value to give the maximum amount of Torque when dying
	public GameObject scorePointsUI; // A prefab of 100 that appears when the enemy dies.

	private EnemyDeath death;
	private EnemyMovement move;
	private SpriteRenderer ren;	// Reference to the sprite renderer.
	private PlayerScore scoreBoard;	// Reference to the Score Script
	private bool dying = false;

	// Use this for initialization
	void Start () 
	{
		self = new GenericEnemy(this.gameObject, health, speed, damage);
		// Setting up the references.
		ren = transform.Find("body").GetComponent<SpriteRenderer>();

		var throwaway = new GameObject();
		throwaway.transform.position = transform.position;
		if (!target)
			target = throwaway.transform;

		death = this.GetComponent<EnemyDeath> ();
		death.die = this;
		move = this.GetComponent<EnemyMovement> ();
		move.location = this.transform;
		move.self = this.self;
		scoreBoard = GameObject.Find("Score").GetComponent<PlayerScore>();
	}

	void FixedUpdate () // Update is called once per frame
	{
		if (dying) { return; }

		if (self.health <= 0)
			Death ();

		RandomTarget ();

		move.TryMove (target); 
	}

	void OnCollisionEnter2D(Collision2D coll)
	{
		if (dying) { return; }

		if (coll.gameObject.tag == "Enemy")
		{
			if (coll.gameObject.name.Contains("Enemy 2") || 
				coll.gameObject.name.Contains("Enemy Mito"))
			{
				Death ();
				createScore ();
			}
		}
		else if (coll.gameObject.tag == "Player")
		{
			// hurt self instead of player
			//Death ();
		}
		else if (coll.gameObject.tag == "Bullet")
		{
			Death ();
			createScore();
		}
	}

	private void RandomTarget ()
	{
		if (Vector2.Distance (target.transform.position, this.transform.position) < 5)
		{
			// random target
			Vector2 i = Random.insideUnitCircle * 25;
			target.position = new Vector3 (i.x + this.transform.position.x,
				i.y + this.transform.position.y);
		}
	}

	private void createScore()
	{
		// Increase the score by so and so points
		scoreBoard.score += self.score;

		// Instantiate the score points prefab at this point.
		GameObject scorePoints = (GameObject) Instantiate(scorePointsUI, Vector3.zero, Quaternion.identity);
		scorePoints.transform.parent = gameObject.transform;
		scorePoints.transform.localPosition = new Vector3(0, 1.5f, 0);
	}

	private void Death ()
	{
		dying = true;
		death.Death (ren, deathSpinMin, deathSpinMax);
	}
}
