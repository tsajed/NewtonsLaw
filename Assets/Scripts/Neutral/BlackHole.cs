using UnityEngine;
using System.Collections;

public class BlackHole : MonoBehaviour {

	float force = 25.0f;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		Vector2 center = renderer.bounds.center;
		Collider2D[] colliders = Physics2D.OverlapCircleAll(renderer.bounds.center, renderer.bounds.extents.x);
		Debug.Log(renderer.bounds.center);
		Debug.Log(renderer.bounds.extents.x);
		foreach(Collider2D hit in colliders) {
			Debug.Log("HIT");
			Rigidbody2D body = hit.rigidbody2D;

			Vector2 dir = transform.position - body.transform.position;
			dir = dir.normalized;
			Debug.Log("DISTANCE: " + dir);
			body.AddForce(dir * force);
		}
	}

	void OnCollisionEnter2D() {

	}
}
