using UnityEngine;
using System.Collections;


public class ScorePoints : MonoBehaviour
{
	void Start () // Use this for initialization
	{
		Invoke("Remove", 1.5f);
	}

	void Update () // Update is called once per frame
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
