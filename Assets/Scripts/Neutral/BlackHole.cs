using UnityEngine;
using System.Collections;

public class BlackHole : MonoBehaviour {

	float force = 25.0f;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		//Vector2 center = renderer.bounds.center;
		Collider2D[] colliders = Physics2D.OverlapCircleAll(renderer.bounds.center, renderer.bounds.extents.x);
		foreach(Collider2D hit in colliders) {
			Rigidbody2D body = hit.rigidbody2D;

			Vector2 dir = transform.position - body.transform.position;
			dir = dir.normalized;
			body.AddForce(dir * force);
		}
	}

	void OnCollisionEnter2D() {

	}
}
