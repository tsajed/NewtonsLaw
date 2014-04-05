using UnityEngine;
using System.Collections;

public class SceneHeatTexts : MonoBehaviour {
	
	string tutorialString = "Heat Seeking enemies will fire homing shots." +
		"These shots act like normal shots, except that they will follow you! Try to lure " +
			"other enemies into their path.";
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
	
	void OnGUI() {
		GUI.skin.textArea.fontSize = 18;
		GUI.TextArea(new Rect(200, Screen.height - 150, Screen.width - 350, 200), tutorialString);
		GUI.FocusControl("");
		if (GUI.Button(new Rect(Screen.width - 150, Screen.height - 50, 150, 50), "Tutorial 7")) {
			Debug.Log("Clicked the button with text");
			Application.LoadLevel(Application.loadedLevel + 1);
		}
	}
	
}
