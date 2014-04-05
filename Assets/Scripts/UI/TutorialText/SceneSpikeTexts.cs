using UnityEngine;
using System.Collections;

public class SceneSpikeTexts : MonoBehaviour {
	
	string tutorialString = "Welcome to the Spike Tutorial. Spike enemies will try to ram" +
		" you. So make sure you push them away" +
		" at all costs. You can block them using other enemies. They are killed by shooting enemy" +
			" shots. Click on the Tuturial 4 button to go to the next tutorial";
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
	
	void OnGUI() {
		GUI.skin.textArea.fontSize = 18;
		tutorialString = GUI.TextArea(new Rect(200, Screen.height - 150, Screen.width - 350, 200), tutorialString);
		if (GUI.Button(new Rect(Screen.width - 150, Screen.height - 50, 150, 50), "Tutorial 4")) {
			Debug.Log("Clicked the button with text");
			Application.LoadLevel(Application.loadedLevel + 1);
		}
	}
	
}
