using UnityEngine;
using System.Collections;

public class ScorePoints : MonoBehaviour {

	// Use this for initialization
	void Start () 
	{
		Invoke("Remove", 1.5f);
	}
	
	// Update is called once per frame
	void Update () 
	{
		Quaternion rotation = Quaternion.identity;
		rotation.eulerAngles = new Vector3(transform.rotation.x, 0, transform.rotation.z);
		gameObject.transform.rotation = rotation;
	}

	void Remove() 
	{
		Destroy(gameObject);
	}
}
