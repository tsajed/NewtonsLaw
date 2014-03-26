using UnityEngine;
using System.Collections;

public class SceneMitoTexts : MonoBehaviour {
	
	string tutorialString = "Welcome to the Mitosis Enemy Tutorial. Mitosis enemies " +
		"create smaller versions of themselves over time and they damage player health" +
		" when in contact with player. So make sure you evade the mitotis enemies" +
			" at all costs. You can use basic enemies to damage mitosis enemies" +
			" when they come in contact. Click on the Tuturial 6 button to go to next tutorial";
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
	
	void OnGUI() {
		tutorialString = GUI.TextArea(new Rect(0, Screen.height - 50, Screen.width - 150, 50), tutorialString);
		if (GUI.Button(new Rect(Screen.width - 150, Screen.height - 50, 150, 50), "Tutorial 6")) {
			Debug.Log("Clicked the button with text");
			Application.LoadLevel(Application.loadedLevel + 1);
		}
	}
	
}
