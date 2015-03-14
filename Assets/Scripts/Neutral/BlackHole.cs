using UnityEngine;
using System.Collections;

public class BlackHole : MonoBehaviour {

	float force = 25.0f;
	// Use this for initialization

	private Renderer rend;
	void Awake() {
		rend = GetComponent<Renderer>();
	}
	
	// Update is called once per frame
	void Update () {
		//Vector2 center = renderer.bounds.center;
		Collider2D[] colliders = Physics2D.OverlapCircleAll(rend.bounds.center, rend.bounds.extents.x);
		foreach(Collider2D hit in colliders) {
			Rigidbody2D body = hit.GetComponent<Rigidbody2D>();

			Vector2 dir = transform.position - body.transform.position;
			dir = dir.normalized;
			body.AddForce(dir * force);
		}
	}

	void OnCollisionEnter2D() {

	}
}
