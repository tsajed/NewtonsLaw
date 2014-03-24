using UnityEngine;
using System.Collections;

public class BlackHole2 : MonoBehaviour {

	float force = 25.0f;

    void OnTriggerStay2D(Collider2D other) {

			Rigidbody2D body = other.rigidbody2D;

			Vector2 dir = transform.position - body.transform.position;
			dir = dir.normalized;
			body.AddForce(dir * force);
    }
}
