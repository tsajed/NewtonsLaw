﻿using UnityEngine;
using System.Collections;

public class PlayerPause : MonoBehaviour {
	bool paused = false;

	// Update is called once per frame
	void Update () {
		if(Input.GetButtonDown("Pause"))
		{
			if(!paused)
			{
				paused = true;
				Time.timeScale = 0;
			}
			else
			{
				paused = false;
				Time.timeScale = 1;
			}
		}
	}
	void OnGUI() 
	{
		if (!paused)
			return;
		
		if (GUI.Button(new Rect(Screen.width/2 - 75, Screen.height/2 - 25, 150, 50), "Restart Level")) 
		{
			Debug.Log("Clicked the button with text");
			Time.timeScale = 1;
			Application.LoadLevel(Application.loadedLevel);
		}
		if (GUI.Button(new Rect(Screen.width/2 - 75, Screen.height/2 + 45, 150, 50), "Go To Next Level")) 
		{
			Debug.Log("Clicked the button with text");
			Time.timeScale = 1;
			Application.LoadLevel(Application.loadedLevel + 1);
		}
	}
}
