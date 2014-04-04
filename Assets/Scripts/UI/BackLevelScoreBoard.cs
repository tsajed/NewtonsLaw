using UnityEngine;
using System.Collections;

public class BackLevelScoreBoard : MonoBehaviour 
{

	private GameObject scoreList;
	private GameObject backButton;

	void OnMouseDown()
	{
		// Go back to Level Scores List Menu
		scoreList.SetActive(true);
		backButton.SetActive(true);
		Destroy(transform.parent.gameObject);
	}

	// Must pass a reference of the inactive Game Objects
	public void SetScoreList(GameObject list, GameObject back) {
		scoreList = list;
		backButton = back;
	}
}
