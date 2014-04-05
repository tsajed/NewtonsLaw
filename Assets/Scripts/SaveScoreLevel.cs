using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class SaveScoreLevel : MonoBehaviour {

	private PlayerScore scoreBoard;	// Reference to the Score Script
	private List<string> scores = new List<string>();
	private int scoreCount = 10;	// The number of ranks for the score board
	private string levelName;
	private string playerName = "Johnny Cake";

	// Use this for initialization
	void Start () 
	{
		levelName = Application.loadedLevelName + "Score";
		scoreBoard = GameObject.Find("Score").GetComponent<PlayerScore>();

		// Parse the saved variables into names and scores separately
		for(int i = 0; i < scoreCount; i++) 
		{

			scores.Add(PlayerPrefs.GetString(levelName + i, "Sean Gouglas:9001"));
		}

		// Debug Load the Level's Current Scoreboard
		for(int k = 0; k < scoreCount; k++)
		{
			for(int j = 0; j < scores.Count; j++)
				Debug.Log(levelName + k + " " + j + ": " + scores[j]);
		}
	}

	// Save the current level score to the Level's Scoreboard
	public void SaveLevelScore() 
	{
		for(int i = 0; i < scoreCount; i++)
		{
			string[] score = PlayerPrefs.GetString(levelName + i, "").Split(char.Parse(":"));
			
			// If the rank does not exist, take its spot
			if(score.Length < 2) 
			{
				PlayerPrefs.SetString(levelName + i, playerName + ":" + scoreBoard.score);
				PlayerPrefs.Save();
				break;
			}

			int scoreInt = int.Parse(score[1]);
			// If old score is larger, then go to next rank
			if(scoreInt >= scoreBoard.score) 
				continue;
			// Else Replace that score and shift others downwards
			else 
			{
				scores.Insert(i, playerName + ":" + scoreBoard.score);
				// If the list gets bigger than 10, remove the last index
				if(scores.Count > 10) 
					scores.RemoveAt(scores.Count-1);

				// Resave all the changes
				for(int k = 0; k < scoreCount; k++) 
				{
					PlayerPrefs.SetString(levelName + k, scores[k]);
				}

				PlayerPrefs.Save();
				break;
			}
		}
		
		// Go to Next Stage
		int index = Application.loadedLevel + 1;
		if(index < Application.levelCount) 
			Application.LoadLevel(index);
	 	else
	 		Application.LoadLevel("StartScreen");

	}

}
