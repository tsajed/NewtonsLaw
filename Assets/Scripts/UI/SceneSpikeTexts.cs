using UnityEngine;
using System.Collections;

public class SceneSpikeTexts : MonoBehaviour {
	
	string tutorialString = "Welcome to the Spike Tutorial. Spike enemies come at you and" +
		" and clash with the main player to reduce health. So make sure you push them away" +
		" at all costs. Use the other enemies if necessary. They are killed by shooting enemy" +
			" shots. Click on the Tuturial 4 button to go to the next tutorial";
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
	
	void OnGUI() {
		tutorialString = GUI.TextArea(new Rect(0, Screen.height - 50, Screen.width - 150, 50), tutorialString);
		if (GUI.Button(new Rect(Screen.width - 150, Screen.height - 50, 150, 50), "Tutorial 4")) {
			Debug.Log("Clicked the button with text");
			Application.LoadLevel(Application.loadedLevel + 1);
		}
	}
	
}
