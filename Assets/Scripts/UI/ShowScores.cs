using UnityEngine;
using System.Collections;

public class ShowScores : MonoBehaviour {

	public GameObject scores;
	
	// Update is called once per frame
	void OnMouseDown()
	{
		scores.SetActive(true);
		transform.parent.gameObject.SetActive(false);
	}
}
