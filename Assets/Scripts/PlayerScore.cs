using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class PlayerScore : MonoBehaviour
{
	public int score = 0;	// The player's score.

	private Text scoreUI;	// The UI Component.

	void Awake() 
	{
		scoreUI = this.GetComponent<Text>();
	}

	void Update ()
	{
		// Set the score text.
		scoreUI.text = "Score: " + score;
	}

}
