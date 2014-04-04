using UnityEngine;
using System.Collections;

// Load the High Scores when Loading the Start Menu
public class LoadScores : MonoBehaviour 
{

	private string[,] levelScores;	// Contains the scores of all the individual levels
	private int sceneCounter;

	void Awake () 
	{
		int counter = 0;
		string[] scores = new string[10];

		// Minus 8 to take account of Tutorials, Credits, and Start Screens
		sceneCounter = Application.levelCount - 8;

		levelScores = new string[sceneCounter, 10];
		for(int k = 0; k < sceneCounter; k++)
		{
			for(int j = 0; j < 10; j++)
			{
				// Score saved in variables such as 'Scene 1Score1', 'Scene 1Score2', etc
				levelScores[k, j] = PlayerPrefs.GetString("Scene " + (k+1) + "Score" + j, "Sean Gouglas:1234");
				
				//Debug.Log("Scene " + (k+1) + "Score" + j + ": " + levelScores[k,j]);
			}
		}
		for(int i = 0; i < 10; i++) 
		{
			// Get the saved string, else default is Sean Gouglas
			scores[i] = PlayerPrefs.GetString("Score"+i, "Sean Gouglas:    9001");
		}

		foreach(Transform child in transform) 
		{
			GUIText scoreText = child.GetComponent("GUIText") as GUIText;

			scoreText.text += scores[counter];
			counter++;
		}
	}
	
}
