using UnityEngine;
using System.Collections;

[RequireComponent (typeof (SaveScoreLevel))]
public class Timer : MonoBehaviour 
{

	public float timer = 200f;

	private SaveScoreLevel saveScore;

	void Awake() 
	{
		saveScore = this.GetComponent<SaveScoreLevel>();
	}

	void Update() 
	{
	  timer -= Time.deltaTime;
	  if (timer <= 0) 
	  {
	  	timer = 0; // clamp the timer to zero
	  	saveScore.SaveLevelScore();

		// Go to Next Stage
		int index = Application.loadedLevel + 1;
		if(index < Application.levelCount) 
			Application.LoadLevel(index);
	 	else
	 		Application.LoadLevel("StartScreen");
	  }
	  
	  int seconds = (int) timer % 60; // calculate the seconds
	  int minutes = (int) timer / 60; // calculate the minutes
	  
	  guiText.text = "Time: " + string.Format("{0:00}:{1:00}", minutes, seconds);
	}
}
