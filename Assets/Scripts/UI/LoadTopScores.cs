using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

public class ScoreComparer: IComparer<TopScore>
{
	public int Compare(TopScore x, TopScore y)
	{
		int score1 = x.score;
		int score2 = y.score;

		// If equal
		if(score1 == score2)
			return 0;

		//If x is greater
		if(score1 > score2)
			return -1;
		//If y is greater
		else
			return 1;
	}
}

public class TopScore {
	public string name { get; set; }
	public int score { get; set; }

	public TopScore(string name, int score) 
	{
		this.name = name;
		this.score = score;
	}
}

// Load the High Scores when Loading the Start Menu
public class LoadTopScores : MonoBehaviour 
{
	private List<TopScore> topScores = new List<TopScore>();
	private ScoreComparer comparer = new ScoreComparer();

	void Awake () 
	{
		// Minus 12 to take account of Tutorials, Credits, and Start Screens
		int sceneCounter = Application.levelCount - 12;

		// Traverse through each level
		for(int i = 0; i < sceneCounter; ++i)
		{	
			// This is the temporary container for getting unique users in a level
			List<string> topTenUnique = new List<string>();

			// Traverse through the top 10 scores of a level
			for(int j = 0; j < 10; ++j)
			{
				// Example string 'playerName:1000'
				string[] score = PlayerPrefs.GetString("Scene " + (i+1) + "Score" + j, "").Split(char.Parse(":"));
				
				// Make sure it's not empty
				if(score.Length > 1)
				{
					// If we already have their best score recorded
					if(topTenUnique.Contains(score[0]))
						continue;
					else 
					{
						// Add it to the top ten unique container
						topTenUnique.Add(score[0]);

						// If player exists in Top Scores, then add it to their overall score
						if(topScores.Any(p => p.name == score[0]))
							topScores.First(d => d.name == score[0]).score += int.Parse(score[1]);
						else
							topScores.Add(new TopScore(score[0], int.Parse(score[1])));
					}
				}
			}
		}

		// Now we must sort topScores by highest overall score
		topScores.Sort(comparer);

		int counter = 0;
		foreach(Transform child in transform)
		{
			if(child.name != "Top Back Text" && counter < topScores.Count)
			{
				foreach(Transform innerChild in child.transform)
				{
					if(innerChild.name == "Name")
					{
						GUIText nameText = innerChild.GetComponent("GUIText") as GUIText;
						nameText.text = topScores[counter].name;
					}
					else if(innerChild.name == "Score")
					{
						GUIText scoreText = innerChild.GetComponent("GUIText") as GUIText;
						scoreText.text = topScores[counter].score.ToString();
					}
				}
				counter++;
			}
		}
	}

	
}
