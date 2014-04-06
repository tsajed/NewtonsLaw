using UnityEngine;
using System.Collections;

public class SceneLaserTexts : MonoBehaviour {
	
	string tutorialString = "Laser enemies " +
		"emit laser in 4 directions that will push you away and damage you. Make sure you are not near the laser" +
			" at all costs. If other enemies hit the laser, they will be flung away, but undamaged. " +
			"You can play different enemies off each other in many different ways, be creative! Click on the Tutorial 5 button to go to next tutorial.";
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
		if (GUI.Button(new Rect(Screen.width - 150, Screen.height - 50, 150, 50), "Tutorial 5")) {
			Debug.Log("Clicked the button with text");
			Application.LoadLevel(Application.loadedLevel + 1);
		}
	}
	
}
