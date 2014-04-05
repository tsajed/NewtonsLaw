using UnityEngine;
using System.Collections;

public class Newton : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		Vector3 p1 = Camera.main.ViewportToWorldPoint(new Vector3(0.06f,0.1f,0));
		p1.z = 0f;
		gameObject.transform.position = p1;
	}
}
