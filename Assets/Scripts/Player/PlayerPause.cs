using UnityEngine;
using System.Collections;

public class PlayerPause : MonoBehaviour {
	bool paused = false;
	GameObject pausebg;
	void Start()
	{
		//Since you can't directly Find() inactive game objects, we 
		//get the UI gameobject and then get the transform from it.
		//Unity is weird sometimes.
		pausebg = GameObject.Find ("UI").transform.FindChild("pausebg").gameObject;

	}
	// Update is called once per frame
	void Update () {
		if(Input.GetButtonDown("Pause"))
		{
			if(!paused)
			{
				paused = true;
				Time.timeScale = 0;
				Vector3 p1 = Camera.main.ViewportToWorldPoint(new Vector3(.5f,.42f,0f));
				p1.z = 0f;
				pausebg.transform.position = p1;
				pausebg.SetActive(true);
			}
			else
			{
				pausebg.SetActive(false);
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
			Time.timeScale = 1;
			Application.LoadLevel(Application.loadedLevel);
		}
		if (GUI.Button(new Rect(Screen.width/2 - 75, Screen.height/2 + 45, 150, 50), "Go To Next Level")) 
		{
			Time.timeScale = 1;
			
			// Go to Next Stage
			int index = Application.loadedLevel + 1;
			if(index < Application.levelCount) 
				Application.LoadLevel(index);
		 	else
		 		Application.LoadLevel("Credits");
		}
		if (GUI.Button(new Rect(Screen.width/2 - 75, Screen.height/2 + 115, 150, 50), "Main Menu")) 
		{
			Time.timeScale = 1;
			Application.LoadLevel("StartScreen");
		}
	}
}
