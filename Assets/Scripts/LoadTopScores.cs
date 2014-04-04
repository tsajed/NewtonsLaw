using UnityEngine;
using System.Collections;

// Load the High Scores when Loading the Start Menu
public class LoadTopScores : MonoBehaviour 
{

	void Awake () 
	{
		int counter = 0;
		string[] scores = new string[10];
		
		for(int i = 0; i < 10; i++) 
		{
			// Get the saved string, else default is Sean Gouglas
			scores[i] = PlayerPrefs.GetString("Score"+i, "Sean Gouglas:    9001");
		}

		foreach(Transform child in transform) 
		{
			GUIText scoreText = child.GetComponent("GUIText") as GUIText;

			if(scoreText.name != "Top Back Text") {
				scoreText.text += scores[counter];
				counter++;
			}
		}
	}
	
}
