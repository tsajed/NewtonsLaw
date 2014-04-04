using UnityEngine;
using System.Collections;

public class ShowLevelScore : MonoBehaviour {

	public Transform levelScoreBoard;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnMouseDown()
	{

		// Turn off the Level Score Board Menu
		transform.parent.gameObject.SetActive(false);
	}
}
