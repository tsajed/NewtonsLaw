using UnityEngine;
using System.Collections;

public class PositionScreen : MonoBehaviour {
	public float x;
	public float y;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		Vector3 p1 = Camera.main.ViewportToWorldPoint(new Vector3(x,y,0));
		p1.z = 0f;
		gameObject.transform.position = p1;
	}
}
